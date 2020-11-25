using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConvocationServer.Websockets
{
    public class WebSocketSession : IDisposable
    {
        private static readonly Random Random = new Random();
        private readonly FrmServer _parent;

        private TcpClient Client { get; }
        private Stream ClientStream { get; }
        public string Id { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }

        public event EventHandler<WebSocketSession> HandshakeCompleted;
        public event EventHandler<WebSocketSession> Disconnected;
        public event EventHandler<Exception> Error;
        public event EventHandler<byte[]> AnyMessageReceived;
        public event EventHandler<string> TextMessageReceived;
        public event EventHandler<string> BinaryMessageReceived;

        public WebSocketSession(TcpClient client, FrmServer parent)
        {
            Client = client;
            _parent = parent;
            ClientStream = client.GetStream();
            Id = Guid.NewGuid().ToString();
            UserName = string.Empty;
            Password = string.Empty;
            LoggedIn = false;
        }

        public void SetUserData(string userName, string password)
        {
            if (userName != string.Empty && userName.Trim().Length > 0)
                UserName = userName;
            if (password != string.Empty && password.Trim().Length > 0)
                Password = password;
        }

        internal void Start()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (!DoHandshake())
                {
                    Error?.Invoke(this, new Exception("Handshake Failed."));
                    Disconnected?.Invoke(this, this);
                    return;
                }

                HandshakeCompleted?.Invoke(this, this);
                StartMessageLoop();
            });
        }

        public void Close()
        {
            if (!Client.Connected) return;

            SendMessage(new byte[] { }, 0x8);

            Client.Close();
        }

        private void StartMessageLoop()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    MessageLoop();
                }
                catch (Exception e)
                {
                    Error?.Invoke(this, e);
                }
                finally
                {
                    Disconnected?.Invoke(this, this);
                }
            });
        }

        private bool DoHandshake()
        {
            while (Client.Available == 0 && Client.Connected) { }
            if (!Client.Connected) return false;

            byte[] handshake;
            using (MemoryStream handshakeBuffer = new MemoryStream())
            {
                while (Client.Available > 0)
                {
                    byte[] buffer = new byte[Client.Available];
                    ClientStream.Read(buffer, 0, buffer.Length);
                    handshakeBuffer.Write(buffer, 0, buffer.Length);
                }

                handshake = handshakeBuffer.ToArray();
            }

            if (!Encoding.UTF8.GetString(handshake).StartsWith("GET")) return false;

            byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                                                  + "Connection: Upgrade" + Environment.NewLine
                                                  + "Upgrade: websocket" + Environment.NewLine
                                                  + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                                                      SHA1.Create().ComputeHash(
                                                          Encoding.UTF8.GetBytes(
                                                              new Regex("Sec-WebSocket-Key: (.*)").Match(Encoding.UTF8.GetString(handshake)).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                                          )
                                                      )
                                                  ) + Environment.NewLine
                                                  + Environment.NewLine);

            ClientStream.Write(response, 0, response.Length);
            return true;
        }

        private void MessageLoop()
        {
            WebSocketSession session = this;
            TcpClient client = session.Client;
            Stream stream = session.ClientStream;

            List<byte> packet = new List<byte>();

            int messageOpcode = 0x0;
            using (MemoryStream messageBuffer = new MemoryStream())
                while (client.Connected)
                {
                    packet.Clear();

                    int ab = client.Available;
                    if (ab == 0) continue;

                    packet.Add((byte)stream.ReadByte());
                    bool fin = (packet[0] & (1 << 7)) != 0;
                    bool rsv1 = (packet[0] & (1 << 6)) != 0;
                    bool rsv2 = (packet[0] & (1 << 5)) != 0;
                    bool rsv3 = (packet[0] & (1 << 4)) != 0;

                    // Must error if is set.
                    //if (rsv1 || rsv2 || rsv3)
                    //    return;

                    int opcode = packet[0] & ((1 << 4) - 1);

                    switch (opcode)
                    {
                        case 0x0: // Continuation Frame
                            break;
                        case 0x1: // Text
                        case 0x2: // Binary
                        case 0x8: // Connection Close
                            messageOpcode = opcode;
                            break;
                        case 0x9:
                            continue; // Ping
                        case 0xA:
                            continue; // Pong
                        default:
                            continue; // Reserved
                    }

                    packet.Add((byte)stream.ReadByte());
                    bool masked = (packet[1] & (1 << 7)) != 0;
                    int pseudoLength = packet[1] - (masked ? 128 : 0);

                    ulong actualLength = 0;
                    if (pseudoLength > 0 && pseudoLength < 125) actualLength = (ulong)pseudoLength;
                    else if (pseudoLength == 126)
                    {
                        byte[] length = new byte[2];
                        stream.Read(length, 0, length.Length);
                        packet.AddRange(length);
                        Array.Reverse(length);
                        actualLength = BitConverter.ToUInt16(length, 0);
                    }
                    else if (pseudoLength == 127)
                    {
                        byte[] length = new byte[8];
                        stream.Read(length, 0, length.Length);
                        packet.AddRange(length);
                        Array.Reverse(length);
                        actualLength = BitConverter.ToUInt64(length, 0);
                    }

                    byte[] mask = new byte[4];
                    if (masked)
                    {
                        stream.Read(mask, 0, mask.Length);
                        packet.AddRange(mask);
                    }

                    if (actualLength > 0)
                    {
                        byte[] data = new byte[actualLength];
                        stream.Read(data, 0, data.Length);
                        packet.AddRange(data);

                        if (masked)
                            data = ApplyMask(data, mask);

                        messageBuffer.Write(data, 0, data.Length);
                    }

                    Console.WriteLine($@"RECV: {BitConverter.ToString(packet.ToArray())}");

                    if (!fin) continue;
                    byte[] message = messageBuffer.ToArray();

                    switch (messageOpcode)
                    {
                        case 0x1:
                            AnyMessageReceived?.Invoke(session, message);
                            TextMessageReceived?.Invoke(session, Encoding.UTF8.GetString(message));
                            break;
                        case 0x2:
                            AnyMessageReceived?.Invoke(session, message);
                            BinaryMessageReceived?.Invoke(session, Encoding.UTF8.GetString(message));
                            break;
                        case 0x8:
                            Close();
                            break;
                        default:
                            throw new Exception("Invalid opcode: " + messageOpcode);
                    }

                    messageBuffer.SetLength(0);
                }
        }

        public void LogIn(string userName, string password)
        {
            LoggedIn = true;
            SetUserData(userName, password);

            // Send out the login message
            SendMessage(message: new JObject {
                            { "service", "status" },
                            { "data", new JObject {
                                { "type", "login" },
                                { "message", $"Logged in as user: {this.UserName}" }
                            } }
                        });
        }

        public void LogOut(bool sendMessage = true)
        {
            string _oldUser = this.UserName;
            LoggedIn = false;
            UserName = string.Empty;
            Password = string.Empty;

            if (sendMessage)
            {
                // Send out the logout message
                SendMessage(message: new JObject {
                                { "service", "status" },
                                { "data", new JObject {
                                    { "type", "logout" },
                                    { "message", $"Logged out as user: {_oldUser}" }
                                } }
                            });
            }
        }

        // * Send a message out with data based on a Xpression request
        public void SendXpressionResponse(string category, string action, dynamic value)
        {
            try
            {
                // Only send XPN responses to the user if they are logged in
                if (!LoggedIn) return;

                if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(value.ToString()))
                {
                    SendMessage(message: new JObject {
                                    { "service", "xpression" },
                                    { "data", new JObject {
                                        { "category", category },
                                        { "action", action },
                                        { "value", value },
                                    }}
                                });
                }
                else
                {
                    string _message = "missing data heading:";
                    // Find out which headings are missing
                    if (string.IsNullOrEmpty(category))
                        _message += "category, ";
                    if (string.IsNullOrEmpty(action))
                        _message += "action, ";
                    if (string.IsNullOrEmpty(value))
                        _message += "value, ";

                    SendMessage(message: new JObject {
                                    { "service", "xpression" },
                                    { "data", new JObject {
                                        { "category", "main" },
                                        { "action", "error" },
                                        { "value", _message.ReplaceLastOccurrence(", ", string.Empty) + "!" },
                                    }}
                                });
                }
            }
            catch (Exception e)
            {
                SendMessage(message: new JObject {
                                { "service", "xpression" },
                                { "data", new JObject {
                                    { "category", "main" },
                                    { "action", "error" },
                                    { "value", e.Message },
                                }}
                            });
                Console.Error.WriteLine(e);
            };
        }

        // * Setup a callback for Clock Widget changes to get sent a relay message
        public bool SetClockWidgetCallback(XPN_Functions xpn, string name, string callback)
        {
            try
            {
                void OnClockChange(int Hours, int Minutes, int Seconds, int Milli)
                {
                    // Only send XPN responses to the user if they are logged in
                    if (!LoggedIn) return;

                    // Send out message on the ClockChange event
                    SendMessage(message: new JObject {
                                    { "service", "relay" },
                                    { "data", new JObject {
                                        { "category", "widget" },
                                        { "action", "SetClockWidgetCallback" },
                                        { "properties", new JObject {
                                            { "name", name },
                                            { "callback", callback },
                                            { "hours", Hours },
                                            { "minutes", Minutes },
                                            { "seconds", Seconds },
                                            { "milli", Milli },
                                        }}
                                    }}
                                });
                }

                return xpn.SetClockWidgetCallback(name: name, callback: OnClockChange);
            }
            catch { return false; }
        }

        public void SendMessage(JObject message)
        {
            try
            {
                if (message != null && message["service"] != null)
                {
                    string _msg = JsonConvert.SerializeObject(message);
                    SendMessage(_msg);
                    string title = message["service"]?.ToString();
                    if (message.ContainsKey("data") && message["data"]["action"] != null)
                        title = message["data"]["action"].ToString();
                    _parent.AddMessage(message, $"{title.FirstCharToUpper()} response", "Outgoing");
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); };
        }

        public void SendMessage(string payload) => SendMessage(Client, payload);
        public void SendMessage(byte[] payload, bool isBinary = false) => SendMessage(Client, payload, isBinary);

        public void SendMessage(byte[] payload, int opcode) => SendMessage(Client, payload, opcode);

        static void SendMessage(TcpClient client, string payload) =>
                    SendMessage(client, Encoding.UTF8.GetBytes(payload), false);
        static void SendMessage(TcpClient client, byte[] payload, bool isBinary = false, bool masking = false)
        {
            SendMessage(client, payload, isBinary ? 0x2 : 0x1);
        }
        static void SendMessage(TcpClient client, byte[] payload, int opcode)
        {
            using (MemoryStream packet = new MemoryStream())
            {
                byte firstbyte = 0b0_0_0_0_0000; // fin | rsv1 | rsv2 | rsv3 | [ OPCODE | OPCODE | OPCODE | OPCODE ]

                firstbyte |= 0b1_0_0_0_0000; // fin
                                             //firstbyte |= 0b0_1_0_0_0000; // rsv1
                                             //firstbyte |= 0b0_0_1_0_0000; // rsv2
                                             //firstbyte |= 0b0_0_0_1_0000; // rsv3

                firstbyte += (byte)opcode; // Text
                packet.WriteByte(firstbyte);

                // Set bit: bytes[byteIndex] |= mask;

                byte secondbyte = 0b0_0000000; // mask | [SIZE | SIZE  | SIZE  | SIZE  | SIZE  | SIZE | SIZE]

                if (payload.LongLength <= 0b0_1111101) // 125
                {
                    secondbyte |= (byte)payload.Length;
                    packet.WriteByte(secondbyte);
                }
                else if (payload.LongLength <= UInt16.MaxValue) // If length takes 2 bytes
                {
                    secondbyte |= 0b0_1111110; // 126
                    packet.WriteByte(secondbyte);

                    byte[] len = BitConverter.GetBytes(payload.LongLength);
                    Array.Reverse(len, 0, 2);
                    packet.Write(len, 0, 2);
                }
                else // if (payload.LongLength <= Int64.MaxValue) // If length takes 8 bytes
                {
                    secondbyte |= 0b0_1111111; // 127
                    packet.WriteByte(secondbyte);

                    byte[] len = BitConverter.GetBytes(payload.LongLength);
                    Array.Reverse(len, 0, 8);
                    packet.Write(len, 0, 8);
                }

                // Write all data to the packet
                packet.Write(payload, 0, payload.Length);

                // Get client's stream
                NetworkStream stream = client.GetStream();

                byte[] finalPacket = packet.ToArray();
                Console.WriteLine($@"SENT: {BitConverter.ToString(finalPacket)}");

                // Send the packet
                foreach (byte b in finalPacket)
                    stream.WriteByte(b);
            }
        }

        static byte[] ApplyMask(IReadOnlyList<byte> msg, IReadOnlyList<byte> mask)
        {
            byte[] decoded = new byte[msg.Count];
            for (int i = 0; i < msg.Count; i++)
                decoded[i] = (byte)(msg[i] ^ mask[i % 4]);
            return decoded;
        }

        public void Dispose()
        {
            Close();

            ((IDisposable)Client)?.Dispose();
            ClientStream?.Dispose();
        }
    }
}
