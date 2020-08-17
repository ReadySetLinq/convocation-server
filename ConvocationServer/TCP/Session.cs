using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConvocationServer.Extensions;
using ConvocationServer.XPN;
using System.Net.Sockets;
using System.Text;

namespace ConvocationServer.TCP
{
    class Session
    {
        private FrmServer _parent;

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }
        public TcpClient TcpClient { get; }
        public Socket TcpSocket { get => TcpClient.Client; }

        public Session(TcpClient client, FrmServer parent)
        {
            TcpClient = client;
            _parent = parent;
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

        public void SendMessage(JObject message)
        {
            try
            {
                if (message != null && message["service"] != null)
                {
                    string _msg = JsonConvert.SerializeObject(message);
                    TcpSocket.Send(Encoding.ASCII.GetBytes(_msg));
                    _parent.AddMessage(message, $"{message["service"]} Response", "Outgoing");
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); };
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

    }

}
