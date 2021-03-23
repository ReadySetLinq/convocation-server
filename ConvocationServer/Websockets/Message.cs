
using System;
using Newtonsoft.Json.Linq;

namespace ConvocationServer.Websockets
{
    [AttributeUsage(AttributeTargets.Property,
                    Inherited = false,
                    AllowMultiple = false)]
    internal sealed class OptionalAttribute : Attribute
    {
    }


    public class Message
    {

        public string Service { get; set; }
        public JObject Data { get; set; }

    }


    // Message services

    public class StatusService
    {

        public string Type { get; set; }
        public string Message { get; set; }

    }


    public class JoinLeaveService
    {

        public string Name { get; set; }

    }

    public class LoginService
    {

        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class LogoutService
    {

        public string UserName { get; set; }

    }

    public class ChatService
    {

        public string UserName { get; set; }
        public string Message { get; set; }

    }

    public class ControllerService
    {

        public string Sport { get; set; }
        public string Action { get; set; }
        public JObject Properties { get; set; }

    }

    public class XpressionService
    {
        public string Category { get; set; }
        public string Action { get; set; }
        public JObject Properties { get; set; }

    }

    public class DatabaseService
    {
        public string UUID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }

        [Optional]
        public JObject Properties { get; set; }

    }

    public class SpectatorService
    {
        public string Action { get; set; }

        public string UUID { get; set; }
        public string Program { get; set; }

        [Optional]
        public JObject Last { get; set; }

        [Optional]
        public JObject Current { get; set; }

        [Optional]
        public JObject Next { get; set; }

    }

}
