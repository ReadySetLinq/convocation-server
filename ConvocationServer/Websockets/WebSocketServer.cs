using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConvocationServer.Websockets
{
    public class WebSocketServer
    {
        private readonly FrmServer parent;
        private TcpListener server;
        private readonly XPN_Functions XpnFunctions = new XPN_Functions();
        private SpectatorService lastSepctator = new SpectatorService();

        public readonly List<WebSocketSession> Clients = new List<WebSocketSession>();
        public event EventHandler<WebSocketSession> ClientConnected;
        public event EventHandler<WebSocketSession> ClientDisconnected;

        public bool IsListening { get; private set; } = false;

        public WebSocketServer(FrmServer serverForm)
        {
            parent = serverForm;
            lastSepctator.Action = "update";
            lastSepctator.UUID = "null";
            lastSepctator.Program = "[none]";
            lastSepctator.Last = "[none]";
            lastSepctator.Current = "[none]";
            lastSepctator.Next = "[none]";
        }
        
        public void Start()
        {
            if (IsListening) return;

            string address = parent.StorageSettings.IPAddress.Trim();
            int port = parent.StorageSettings.Port;

            if (address.Length == 0 || port <= 0)
            {
                parent.UpdateStatus("Settings Error");
                MessageBox.Show("You must select a valid IP and Port!", "Invalid connection settings",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Make sure Xpression is started and linked before running the server
            if (!XpnFunctions.Start())
            {
                parent.UpdateStatus("Xpression Error");
                DialogResult response = MessageBox.Show("Make sure Xpression is running before starting the server!", "Failed to connect with Xpression",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                parent.UpdateStatus("Stopped");
                return;
            }

            string projectName = XpnFunctions.ProjectFileName();
            if (projectName == null)
            {
                bool projectLoaded = false;
                projectName = parent.SelectXpnProjectPath();
                if (projectName != null)
                {
                    projectLoaded = XpnFunctions.LoadProject(projectName);
                }

                if (!projectLoaded)
                {
                    XpnFunctions.Dispose();
                    parent.UpdateStatus("Xpression Error");
                    DialogResult response = MessageBox.Show("Make sure a Xpression Project is open before starting the server!", "Failed to connect with Xpression",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    parent.UpdateStatus("Stopped");
                    return;
                }
            }

            server = new TcpListener(IPAddress.Parse(address), Convert.ToInt32(port));
            server.Start();

            JObject message = new JObject { { "IP", address }, { "Port", port } };
            parent.AddMessage(message, "Server Started", "Outgoing");
            parent.UpdateStatus("Running");
            IsListening = true;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (IsListening)
                {
                    try
                    {
                        WebSocketSession session = new WebSocketSession(server.AcceptTcpClient(), parent);
                        session.HandshakeCompleted += (__, ___) =>
                        {
                            Client_Connected(session);
                        };

                        session.Disconnected += (__, ___) =>
                        {
                            Client_Disconnected(session);
                        };

                        session.TextMessageReceived += (__, ___) =>
                        {
                        // __ = WebSocketSession
                        // ___ = message
                        Client_TextMessageReceived(session, ___);
                        };
                        ClientConnected?.Invoke(this, session);
                        session.Start();
                    } catch { }
                }
            });
        }

        public void Stop()
        {
            JObject message = new JObject { 
                { "IP", parent.StorageSettings.IPAddress.Trim() }, 
                { "Port", parent.StorageSettings.Port } 
            };
            parent.AddMessage(message, "Server Stopped", "Outgoing");
            server.Stop();
            parent.UpdateStatus("Stopped");
            IsListening = false;
        }

        public void SendTo(WebSocketSession session, JObject message)
        {
            try
            {
                if (!IsListening) return;

                if (message != null && message["service"] != null)
                {
                    session.SendMessage(message);
                    string title = message["service"]?.ToString();
                    if (message.ContainsKey("data") && message["data"]["action"] != null)
                        title = message["data"]["action"].ToString();
                    parent.AddMessage(message, $"{title.FirstCharToUpper()} Message", "Outgoing");
                }
            }
            catch (Exception e)
            {
                parent.AddMessage(JObject.FromObject(e), $"Error: {e.Message}", "Outgoing");
            };
        }

        // Send a message to everyone in a given service
        private void SendToAll(JObject message, bool verifyLogin = true, bool verifySpectator = false, bool addLog = true)
        {
            try
            {
                if (!IsListening) return;

                if (message != null && message["service"] != null)
                {
                    string _msg = JsonConvert.SerializeObject(message);
                    // Loop through all connections
                    foreach (WebSocketSession session in Clients)
                    {
                        // Only send out messages to verified connections
                        if ((!verifyLogin || session.LoggedIn) && (!verifySpectator || session.IsSpectator))
                        {
                            session.SendMessage(_msg);
                        }
                    }
                }
                if (addLog)
                {
                    string title = message["service"]?.ToString();
                    if (message.ContainsKey("data") && message["data"]["action"] != null)
                        title = message["data"]["action"].ToString();
                    parent.AddMessage(message, $"{title.FirstCharToUpper()} Message", "Outgoing");
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
        private WebSocketSession GetSession(WebSocketSession session)
        {
            return (WebSocketSession)Clients.Where(s => s.Id == session.Id);
        }

        private void Client_Connected(WebSocketSession session)
        {
            parent.AddMessage(JObject.FromObject(session), $"Client Connected", "Incoming");
            Clients.Add(session);
            session.SendMessage(message: new JObject
                        {
                            { "service", "server" },
                            { "data", new JObject {
                                { "message", "connected" }
                            }}
                        });
        }

        private void Client_Disconnected(WebSocketSession session)
        {
            parent.AddMessage(JObject.FromObject(session), $"Client Disconnected", "Incoming");
            session.LoggedIn = false;
            Clients.Remove(session);

            ClientDisconnected?.Invoke(this, session);
            session.Dispose();
        }

        private void Client_TextMessageReceived(WebSocketSession session, string message)
        {
            try
            {
                JObject _msgObj = message.Trim().ValidateJSON();
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
                                                            { "message", $"You must be logged in to access the service: {sMessage.Service}" }
                                                        } }
                                                    });
                            }
                            break;

                        // Spectator
                        case "spectator":
                            sData = sMessage.Data.ToObject<SpectatorService>();
                            // If it's an update action, send spectator message to all
                            if (sData.Action == "update")
                            {
                                // Example: {"service": "spectator", "data": {"action": "update", "uuid": "spectatorUpdate-123", "program": "BRTF", "last": {"id": 1, "name": "First Last", extra: "", "multiplier": 0, "displyName": "First Last" }, "current": {"id": 2, "name": "First Last", extra: "", "multiplier": 0, "displyName": "First Last" }, "next": {"id": 3, "name": "First Last", extra: "", "multiplier": 0, "displyName": "First Last" }} }
                                if (session.LoggedIn)
                                {
                                    // Update local lastSepctator variable
                                    lastSepctator = sData;
                                    // Send out message to everyone who is a spectator
                                    SendToAll(message: new JObject {
                                                { "service", "spectator" },
                                                { "data", new JObject {
                                                    { "program", sData?.Program },
                                                    { "last", sData?.Last },
                                                    { "current", sData?.Current },
                                                    { "next", sData?.Next },
                                                } }
                                            },
                                            verifyLogin: false,
                                            verifySpectator: true,
                                            addLog: true);
                                }
                                else
                                {
                                    parent.AddMessage(_msgObj, "Error: Invalid login status", "Incoming");
                                    session.SendMessage(message: new JObject {
                                                            { "service", "status" },
                                                            { "data", new JObject {
                                                                { "type", "error" },
                                                                { "message", $"You must be logged in to access the service: {sMessage.Service}, with the action: {sData.Action}" }
                                                            } }
                                                        });
                                }
                            } else if (sData.Action == "join")
                            {
                                // If its a join message, send the last logged spectator message
                                // Example: {"service": "spectator", "data": {"action": "join", "uuid": "spectatorUpdate-123"} }
                                parent.AddMessage(_msgObj, "Spectator joined", "Incoming");
                                session.IsSpectator = true;
                                session.SendMessage(message: new JObject {
                                                        { "service", "spectator" },
                                                        { "data", new JObject {
                                                            { "program", lastSepctator?.Program },
                                                            { "last", lastSepctator?.Last },
                                                            { "current", lastSepctator?.Current },
                                                            { "next", lastSepctator?.Next },
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
                                session.SendMessage(message: new JObject {
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
