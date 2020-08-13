
namespace ConvocationServer.Storage
{
    public class Account
    {
        private string _userName = "user";

        public string UserName { get => _userName; set => _userName = value.Trim(); }
        public string Password { get; set; } = "password";

        public Account(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string LowerName { get => _userName.ToLower(); }
    }
}
