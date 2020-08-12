using System;
using System.Collections.Generic;
using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using Fleck;
using Newtonsoft.Json.Linq;

namespace ConvocationServer.Sockets
{
    class SocketServer
    {
        private readonly XPN_Functions XpnFunctions = new XPN_Functions();
        private List<SocketClient> Clients = new List<SocketClient>();
        private WebSocketServer Server;
        private const bool T = true;
        private const bool F = false;
        public string Address { get; set; }
        public int Port { get; set; }

        public Boolean IsConnected { get; set; }

        public SocketServer() 
        {
            this.IsConnected = false;
            this.Address = "";
            this.Port = -1;
        }

        public void SetData(string address, int port)
        {
            this.Address = address;
            this.Port = port;
        }

        public bool Start()
        {
            if (this.Address.Length == 0 || this.Port == -1) return false;

            this.Server = new WebSocketServer($"{"ws"}://{this.Address}:{this.Port}")
            {
                RestartAfterListenError = T
            };
            Server.ListenerSocket.NoDelay = T;

            Server.Start(conn =>
            {
                conn.OnOpen = () =>
                {
                    if (Globals.DEBUG_MODE)
                        Console.WriteLine("[Debug] Client Connected!");

                    SocketClient _newClient = new SocketClient(conn);
                    Clients.Add(_newClient);
                    _newClient.SendMessage(message: new JObject
                        {
                            { "service", "server" },
                            { "data", new JObject {
                                { "message", "connected" }
                            }}
                        });
                };

                conn.OnClose = () =>
                {
                    if (Globals.DEBUG_MODE)
                        Console.WriteLine("[Debug] Client Disconnected!");
                    int index = Clients.FindIndex(a => a.Socket == conn);
                    if (index != -1)
                    {
                        Clients[index].LeaveAllServices();
                        Clients.RemoveAt(index);
                    }
                };

                conn.OnMessage = message =>
                {
                    if (Globals.DEBUG_MODE)
                        Console.WriteLine($"[Debug] IN: {message}");

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
                                                    },
                                                    isListening: false);
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
                                        if (_client.LoggedIn && _client.IsListeningTo("xpression"))
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
                                                                },
                                                                isListening: false);
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
                                                                },
                                                                isListening: false);
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
                                                                },
                                                                isListening: false);
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
                                                                },
                                                                isListening: false);
                                        }
                                        break;

                                    case "join":
                                        // Example: {"service": "join", "data": {"name": "chat"} }
                                        if (_client.LoggedIn)
                                        {
                                            sData = sMessage.Data.ToObject<JoinLeaveService>();
                                            if (sData.Name != null && sData.Name.Length > 0)
                                            {
                                                // Set the service to the channels listening list
                                                _client.JoinService(service: sData.Name);
                                            }
                                        }
                                        else
                                        {
                                            _client.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "You must be logged in before joining a service!" }
                                                                    } }
                                                                },
                                                                isListening: false);
                                        }
                                        break;

                                    case "leave":
                                        // Example: {"service": "leave", "data": {"name": "chat"} }
                                        if (_client.LoggedIn)
                                        {
                                            sData = sMessage.Data.ToObject<JoinLeaveService>();
                                            if (sData.Name != null && sData.Name.Length > 0)
                                            {
                                                // Do not allow leaving the defalt services
                                                if (sData.Name.Equals("server") || sData.Name.Equals("status") || sData.Name.Equals("relay")) return;

                                                // Remove the service from the channels listening list
                                                _client.LeaveService(service: sData.Name);
                                            }
                                        }
                                        else
                                        {
                                            _client.SendMessage(message: new JObject {
                                                                    { "service", "status" },
                                                                    { "data", new JObject {
                                                                        { "type", "error" },
                                                                        { "message", "You must be logged in before leaving a service!" }
                                                                    } }
                                                                },
                                                                isListening: false);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception e) { Console.Error.WriteLine(e); };
                };
                this.IsConnected = true;
            });
            return true;
        }

        public void Stop()
        {
            // *  Send all connections a shutdown message
            this.SendToAll(message: new JObject
            {
                { "service", "server" },
                { "data", new JObject {
                    { "message", "shutdown" }
                }}
            }, verifyLogin: F);

            this.Server.Dispose();
            this.IsConnected = false;
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
        private static bool IsValidLogin(string userName, string password)
        {
            return userName != null && userName.ToLower() == "brtf" &&
                password != null && password.ToLower() == "brtfuser"
                ? T : F;
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
    }
}
