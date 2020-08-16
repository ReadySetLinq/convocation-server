using System;
using System.Collections.Generic;
using System.Threading;
using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using Fleck;
using Newtonsoft.Json.Linq;

namespace ConvocationServer.Sockets
{
    class SocketServer
    {
        private readonly FrmServer _parent;
        private Thread _threadingServer = null;
        private readonly XPN_Functions XpnFunctions = new XPN_Functions();
        private readonly List<SocketClient> Clients = new List<SocketClient>();
        private WebSocketServer Server = null;
        private bool runServer = false;
        private const bool T = true;
        private const bool F = false;

        public bool IsConnected { get; set; }

        public SocketServer(FrmServer parent) 
        {
            _parent = parent;
            IsConnected = false;
            _threadingServer = null;
        }

        ~SocketServer()
        {
            if (Server != null)
                Server.Dispose();
        }

        public void Start()
        {
            string address = _parent.StorageSettings.IPAddress.Trim();
            int port = _parent.StorageSettings.Port;

            if (address.Length == 0 || port <= 0) return;

            runServer = true;
            if (_threadingServer != null)
                _threadingServer.Abort();

            _threadingServer = new Thread(RunServer)
            {
                IsBackground = true
            };
            _threadingServer.Start();
        }

        public void Stop()
        {
            // *  Send all connections a shutdown message
            SendToAll(message: new JObject
            {
                { "service", "server" },
                { "data", new JObject {
                    { "message", "shutdown" }
                }}
            }, verifyLogin: F);

            CancelServer();
        }

        private void RunServer()
        {
            try
            {
                string address = _parent.StorageSettings.IPAddress.Trim();
                int port = _parent.StorageSettings.Port;
                while (runServer)
                {
                    if (Server == null)
                    {
                        Server = new WebSocketServer($"{"ws"}://{address}:{port}")
                        {
                            RestartAfterListenError = T
                        };
                        Server.ListenerSocket.NoDelay = T;

                        Server.Start(socket =>
                        {
                            socket.OnOpen = () => OnOpen(socket);
                            socket.OnClose = () => OnClose(socket);
                            socket.OnError = exception => Console.Error.WriteLine(exception);
                            socket.OnMessage = message => OnMessage(socket, message);

                            IsConnected = false;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            };
        }

        private void CancelServer()
        {
            runServer = false;
            Server.Dispose();
            Server = null;
            IsConnected = false;
        }

        // Send a message to everyone in a given service
        private void SendToAll(JObject message, bool verifyLogin = T)
        {
            try
            {
                if (message != null && message["service"] != null)
                {
                    // Loop through all connections
                    foreach (SocketClient client in Clients)
                    {
                        // Only send out messages to verified connections
                        if (!verifyLogin || client.LoggedIn == T)
                        {
                            client.SendMessage(message: message);
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); };
        }

        // Verify login information given
        private bool IsValidLogin(string userName, string password)
        {
            return userName != null && 
                password != null && 
                _parent.StorageSettings.IsValidAccountLogin(userName, password);
        }

        // Get a specific SocketClient based on its socket connection info ID
        private SocketClient GetClient(IWebSocketConnection connection)
        {
            SocketClient _client = null;

            Clients.ForEach(client =>
            {
                if (_client == null)
                {
                    if (client.Socket.ConnectionInfo.Id == connection.ConnectionInfo.Id)
                        _client = client;
                }
            });

            return _client;
        }

        private void OnOpen(IWebSocketConnection conn)
        {
            _parent.AddMessage("Client Connected", "New connection", "Incoming");

            SocketClient _newClient = new SocketClient(conn, _parent);
            Clients.Add(_newClient);
            _newClient.SendMessage(message: new JObject
                    {
                        { "service", "server" },
                        { "data", new JObject {
                            { "message", "connected" }
                        }}
                    });
        }

        private void OnClose(IWebSocketConnection conn)
        {
            _parent.AddMessage("Client Disconnected", "Disconnection", "Incoming");

            int index = Clients.FindIndex(client => client.Socket == conn);
            if (index != -1)
                Clients.RemoveAt(index);
        }

        private void OnMessage(IWebSocketConnection conn, string message)
        {
            _parent.AddMessage(message, "Client Message", "Incoming");

            try
            {
                SocketClient _client = GetClient(conn);
                if (_client != null)
                {

                    JObject _msgObj = message.ValidateJSON();
                    if (_msgObj == null)
                    {
                        _client.SendMessage(message: new JObject {
                                                        { "service", "status" },
                                                        { "data", new JObject {
                                                            { "type", "error" },
                                                            { "message", $"Invalid message" }
                                                        } }
                                                    });
                    }
                    SocketMessage sMessage = _msgObj.ToObject<SocketMessage>();

                    if (sMessage.Service != null && sMessage.Data != null)
                    {
                        dynamic sData = null;

                        switch (sMessage.Service.ToLower())
                        {
                            // Xpression
                            case "xpression":
                                // Example: {"service": "xpression", "data": {"category": "takeitem", "action": "GetTakeItemStatus", "properties": {"takeID": 1 }} }
                                if (_client.LoggedIn)
                                {
                                    sData = sMessage.Data.ToObject<XpressionService>();
                                    XPN_Events.Execute(XPN: XpnFunctions, client: _client, data: sData);
                                }
                                else
                                {
                                    _client.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", $"You must be logged in and listening to the service: {sMessage.Service}" }
                                                                    } }
                                                                });
                                }
                                break;

                            case "status":
                                // Example: {"service": "status", "data": {"type": "ping"}}
                                sData = sMessage.Data.ToObject<StatusService>();
                                if (sData.Type != null && sData.Type == "ping")
                                {
                                    _client.SendMessage(message: new JObject
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
                                    _client.SendMessage(message: new JObject {
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
                                sData = sMessage.Data.ToObject<LoginService>();
                                if (IsValidLogin(userName: sData.UserName, password: sData.Password))
                                    _client.LogIn(sData.UserName, sData.Password);
                                else
                                {
                                    _client.SendMessage(message: new JObject {
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
                                if (_client.LoggedIn)
                                {
                                    sData = sMessage.Data.ToObject<LogoutService>();
                                    // Make sure the user didn't try and log someone else out
                                    if (_client.UserName.Equals(sData.UserName, StringComparison.OrdinalIgnoreCase))
                                        _client.LogOut();
                                }
                                else
                                {
                                    _client.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "No login detected!" }
                                                                    } }
                                                                });
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); };
        }
    }
}
