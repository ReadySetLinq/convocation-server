using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;

namespace ConvocationServer.TCP
{
    class Server
    {
        private readonly FrmServer parent;
        private readonly XPN_Functions XpnFunctions = new XPN_Functions();

        private SimpleTcpServer server = null;
        private List<Session> Sessions = new List<Session>();

        public bool IsConnected { get; set; }

        public Server(FrmServer serverForm) 
        {
            parent = serverForm;
            IsConnected = false;
        }

        ~Server()
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        public void Start()
        {
            if (server != null) return;

            string address = parent.StorageSettings.IPAddress.Trim();
            int port = parent.StorageSettings.Port;

            if (address.Length == 0 || port <= 0) return;

            Sessions = new List<Session>();
            server = new SimpleTcpServer
            {
                Delimiter = 0x13, // enter
                StringEncoder = Encoding.ASCII,
            };
            server.DataReceived += Server_DataReceived;
            server.ClientConnected += Client_Connected;
            server.ClientDisconnected += Client_Disconnected;
            server.Start(IPAddress.Parse(address), Convert.ToInt32(port));

            JObject message = new JObject { { "IP", address },  { "Port", port } };
            parent.AddMessage(message, "Server Started!", "Outgoing");
            IsConnected = true;
        }

        public void Stop()
        {
            // *  Send all connections a shutdown message
            JObject message = new JObject
            {
                { "service", "server" },
                { "data", new JObject {
                    { "message", "shutdown" }
                }}
            };

            SendToAll(message: message, verifyLogin: false, addLog: false);
            parent.AddMessage(message, "Server Shutdown", "Outgoing");

            // Wait 3 seconds to make sure all clients get the shutdown message
            Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith((task) => {
                server.Stop();
                server = null;
                IsConnected = false;
            });
        }

        public void SendTo(Socket socket, JObject message)
        {
            try
            {
                if (server == null || !server.IsStarted) return;
                if (!IsConnected) return;

                if (message != null && message["service"] != null)
                {
                    string _msg = JsonConvert.SerializeObject(message);
                    socket.Send(Encoding.ASCII.GetBytes(_msg));
                    parent.AddMessage(message, $"{message["service"]} Message", "Outgoing");
                }
            }
            catch (Exception e)
            {
                parent.AddMessage(JObject.FromObject(e), $"Error: {e.Message}", "Outgoing");
            };
        }

        // Send a message to everyone in a given service
        private void SendToAll(JObject message, bool verifyLogin = true, bool addLog = true)
        {
            try
            {
                if (server == null || !server.IsStarted) return;
                if (!IsConnected) return;

                if (message != null && message["service"] != null)
                {
                    string _msg = JsonConvert.SerializeObject(message);
                    if (verifyLogin)
                    {
                        // Loop through all connections
                        foreach (Session session in Sessions)
                        {
                            // Only send out messages to verified connections
                            if (session.LoggedIn == true)
                            {
                                session.TcpSocket.Send(Encoding.ASCII.GetBytes(_msg));
                            }
                        }
                    } else
                    {
                        // Send to everyone
                        server.Broadcast(Encoding.ASCII.GetBytes(_msg));
                    }
                    if (addLog)
                        parent.AddMessage(message, $"{message["service"]} Message", "Outgoing");
                }
            }
            catch (Exception e)
            {
                parent.AddMessage(JObject.FromObject(e), $"Error: {e.Message}", "Outgoing");
            };
        }

        // Verify login information given
        private bool IsValidLogin(string userName, string password)
        {
            return userName != null && 
                password != null && 
                parent.StorageSettings.IsValidAccountLogin(userName, password);
        }

        // Get a specific SocketClient based on its socket connection info ID
        private Session GetSession(Session session)
        {
            return (Session)Sessions.Where(s => s.TcpClient == session.TcpClient);
        }

        private void Client_Connected(object sender, TcpClient e)
        {
            Session _newSession = new Session(e, parent);
            Sessions.Add(_newSession);
            _newSession.SendMessage(message: new JObject
                    {
                        { "service", "server" },
                        { "data", new JObject {
                            { "message", "connected" }
                        }}
                    });
        }

        private void Client_Disconnected(object sender, TcpClient e)
        {
            int index = Sessions.FindIndex(session => session.TcpClient == e);
            if (index != -1)
                Sessions.RemoveAt(index);
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            string message = e.MessageString.Trim();
            int clientIndex = Sessions.FindIndex(c => c.TcpClient == e.TcpClient);
            if (clientIndex < 0) return;
            try
            {
                Session session = Sessions[clientIndex];
                JObject _msgObj = message.ValidateJSON();
                if (_msgObj == null)
                {
                    session.SendMessage(message: new JObject {
                                                        { "service", "status" },
                                                        { "data", new JObject {
                                                            { "type", "error" },
                                                            { "message", $"Invalid message" }
                                                        } }
                                                    });
                    parent.AddMessage(message, "Error: Invalid client message", "Incoming");
                    return;
                }
                Message sMessage = _msgObj.ToObject<Message>();

                if (sMessage.Service != null && sMessage.Data != null)
                {
                    dynamic sData = null;

                    switch (sMessage.Service.ToLower())
                    {
                        // Xpression
                        case "xpression":
                            // Example: {"service": "xpression", "data": {"category": "takeitem", "action": "GetTakeItemStatus", "properties": {"takeID": 1 }} }
                            if (session.LoggedIn)
                            {
                                sData = sMessage.Data.ToObject<XpressionService>();
                                XPN_Events.Execute(parent: parent, xpn: XpnFunctions, session: session, data: sData);
                            }
                            else
                            {
                                parent.AddMessage(_msgObj, "Error: Invalid login status", "Incoming");
                                session.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", $"You must be logged in and access the service: {sMessage.Service}" }
                                                                    } }
                                                                });
                            }
                            break;

                        case "status":
                            // Example: {"service": "status", "data": {"type": "ping"}}
                            parent.AddMessage(_msgObj, "Status request", "Incoming");
                            sData = sMessage.Data.ToObject<StatusService>();
                            if (sData.Type != null && sData.Type == "ping")
                            {
                                session.SendMessage(message: new JObject
                                                {
                                                    { "service", "status" },
                                                    { "data", new JObject {
                                                        { "type", "ping" },
                                                        { "message", "pong" }
                                                    }}
                                                });
                            }
                            else
                            {
                                session.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "Invalid status type!" }
                                                                    } }
                                                                });
                            }
                            break;

                        case "login":
                            // Example: {"service": "login", "data": {"userName": "root", "password": "admin"} }
                            // Make sure the user gave a valid login
                            parent.AddMessage(_msgObj, "Login request", "Incoming");
                            sData = sMessage.Data.ToObject<LoginService>();
                            if (IsValidLogin(userName: sData.UserName, password: sData.Password))
                            {
                                session.LogIn(sData.UserName, sData.Password);
                            }
                            else
                            {
                                session.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "Incorrect username or password!" }
                                                                    } }
                                                                });
                            }

                            break;

                        case "logout":
                            // Example: {"service": "logout", "data": {"userName": "root"} }
                            parent.AddMessage(_msgObj, "Logout request", "Incoming");
                            if (session.LoggedIn)
                            {
                                sData = sMessage.Data.ToObject<LogoutService>();
                                // Make sure the user didn't try and log someone else out
                                if (session.UserName.Equals(sData.UserName, StringComparison.OrdinalIgnoreCase))
                                    session.LogOut();
                            }
                            else
                            {
                                session.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "No login detected!" }
                                                                    } }
                                                                });
                            }
                            break;
                        default:
                            parent.AddMessage(_msgObj, "Warning: Unknown client message", "Incoming");
                            break;
                    }
                }
                else
                    parent.AddMessage(_msgObj, "Error: Invalid client message", "Incoming");

            }
            catch (Exception ex)
            {
                parent.AddMessage(JObject.FromObject(ex), $"Error: {ex.Message}", "Incoming");
            };
        }

    }
}
