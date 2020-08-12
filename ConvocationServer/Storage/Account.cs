
namespace ConvocationServer.Storage
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Account(string userName, string password)
        {
            UserName = userName.Trim();
            Password = password;
        }

        public string LowerName { get => this.UserName.ToLower(); }
    }
}
