using ConvocationServer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ConvocationServer.Storage
{
    class AppSettings
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public List<Account> Accounts { get; set; }

        public AppSettings()
        {
            // Set Defaults
            IPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            Port = 8181;
            Accounts = new List<Account>();
        }

    }

    public class Settings
    {
        private AppSettings appSettings;
        private readonly string fileName;

        public string IPAddress { get => appSettings.IPAddress; set => appSettings.IPAddress = value; }
        public int Port { get => appSettings.Port; set => appSettings.Port = value; }
        public List<Account> Accounts { get => appSettings.Accounts; set => appSettings.Accounts = value; }

        public Settings()
        {
            // Set Defaults
            appSettings = new AppSettings();
            fileName = $"{AppDomain.CurrentDomain.BaseDirectory}0001.rsl";
        }

        public void Load()
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);
            }
            catch
            {
                appSettings = new AppSettings();
            }
        }

        public void Save()
        {
            // Seralize appSettings class into JSON string
            string jsonString = JsonSerializer.Serialize(appSettings);
            // Write data to our file
            File.WriteAllText(fileName, jsonString);
        }

        public bool AddAccount(string userName, string password)
        {
            string _userName = userName.ToLower().Trim();

            // Make sure the userName doesn't already exist
            if (Accounts.FindIndex(a => a.LowerName.Equals(_userName)) != -1) return false;

            Accounts.Add(new Account(userName, password.Base64Encode()));
            return true;
        }

        public bool RemoveAccount(string userName)
        {
            string _userName = userName.ToLower().Trim();
            int index = Accounts.FindIndex(a => a.LowerName.Equals(_userName));

            // Make sure the userName exist
            if (index == -1) return false;

            Accounts.RemoveAt(index);
            return true;
        }

        public bool EditAccountPassword(string userName, string newPassword)
        {
            string _userName = userName.ToLower().Trim();
            Account account = Accounts.FirstOrDefault(a => a.LowerName.Equals(_userName));

            // Make sure an account was found
            if (account == null) return false;

            account.Password = newPassword;
            return true;
        }

        public bool IsValidAccountLogin(string userName, string password)
        {
            string _userName = userName.ToLower().Trim();
            Account account = Accounts.FirstOrDefault(a => a.LowerName.Equals(_userName));

            // Make sure an account was found
            if (account == null) return false;

            // Return if the password matches
            return (account.Password.Equals(password.Base64Encode()));
        }

    }
}
