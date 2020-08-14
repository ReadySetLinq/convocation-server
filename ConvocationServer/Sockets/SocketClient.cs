using System;
using System.Collections.Generic;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConvocationServer.Extensions;
using ConvocationServer.XPN;

namespace ConvocationServer.Sockets
{

    class SocketClient
    {
        private List<string> Services;
        private FrmServer _parent;
        public IWebSocketConnection Socket { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }

        public SocketClient(IWebSocketConnection socket, FrmServer parent)
        {
            Socket = socket;
            _parent = parent;
            UserName = string.Empty;
            Password = string.Empty;
            LoggedIn = false;
            // Join user to default services
            Services = new List<string>()
            {
                "server",
                "status",
                "relay"
            };
        }

        public void SetUserData(string userName, string password)
        {
            if (userName != string.Empty && userName.Trim().Length > 0)
                UserName = userName;
            if (password != string.Empty && password.Trim().Length > 0)
                Password = password;
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
                        },
                        isListening: false);
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
                            },
                            isListening: false);
            }

            // Remove the user from services requiring a valid login
            ResetServices(sendMessage: sendMessage);
        }

        // Return True Or False depending on if the given service is in our Services list.
        public bool IsListeningTo(string service)
        {
            string _service = service.ToLower().Trim();

            return Services.FindIndex(clientService => _service.Equals(clientService)) != -1;
        }


        public void JoinService(string service)
        {
            string _service = service.ToLower().Trim();
            int index = Services.FindIndex(clientService => _service.Equals(clientService));
            // Make sure we aren't already in this service
            if (index == -1)
            {
                Services.Add(_service);
                // Send out confirmation message
                SendMessage(message: new JObject {
                                { "service", "status" },
                                { "data", new JObject {
                                    { "type", "joined" },
                                    { "message", $"Service: {_service}" }
                                } }
                            },
                            isListening: false);
            }
        }


        public void LeaveService(string service, bool sendMessage = true)
        {
            string _service = service.ToLower().Trim();

            // Prevent leaving the core default services
            if (service.Equals("server") || service.Equals("status") || service.Equals("relay"))
                return;

            int index = Services.FindIndex(clientService => _service.Equals(clientService));
            // Make sure we are already in this service
            if (index != -1)
            {
                Services.RemoveAt(index);
                if (sendMessage)
                {
                    // Send out confirmation message
                    SendMessage(message: new JObject {
                                    { "service", "status" },
                                    { "data", new JObject {
                                        { "type", "left" },
                                        { "message", $"Service: {_service}" }
                                    } }
                                },
                                isListening: false);
                }
            }
        }

        public void ResetServices(bool sendMessage = true)
        {
            // Use a temp list to avoid errors when removing items from the list while iterating through it.
            List<string> _services = new List<string>(Services);
            // Reset service list to default
            _services.ForEach(service =>
            {
                if (!service.Equals("server") && !service.Equals("status") && !service.Equals("relay"))
                    LeaveService(service: service, sendMessage: sendMessage);
            });
        }

        public void LeaveAllServices()
        {
            // Use a temp list to avoid errors when removing items from the list while iterating through it.
            List<string> _services = new List<string>(Services);
            // Clear service list to default
            _services.ForEach(service => LeaveService(service: service, sendMessage: false));
        }


        public void SendMessage(JObject message, bool isListening = true)
        {
            try
            {
                if (message != null && message["service"] != null)
                {
                    // Make sure the client is listening to the service for this message unless disabled
                    if (!isListening || IsListeningTo(message["service"].ToString()))
                    {
                        string _msg = JsonConvert.SerializeObject(message);
                        Socket.Send(_msg);
                        _parent.AddMessage(_msg, $"{message["service"]} Response", "Outgoing");
                    }
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); };
        }

        // * Send a websocket message out with data based on a Xpression request
        public void SendXpressionResponse(string category, string action, dynamic value)
        {
            try
            {
                if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(value.ToString()))
                {
                    SendMessage(message: new JObject {
                                    { "service", "xpression" },
                                    { "data", new JObject {
                                        { "category", category },
                                        { "action", action },
                                        { "value", value },
                                    }}
                                }, isListening: false);
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
                                }, isListening: false);
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

    }

}
