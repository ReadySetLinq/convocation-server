using ConvocationServer.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Win32;
using System.Windows.Forms;

namespace ConvocationServer.Storage
{
    class AppSettings
    {
        private readonly string _defaultIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public bool RunOnStartup { get; set; }
        public List<Account> Accounts { get; set; }


        public AppSettings()
        {
            // Set Defaults
            IPAddress = _defaultIpAddress;
            Port = 8181;
            RunOnStartup = true;
            Accounts = new List<Account>();
        }

        public string SetIPAddress(string address)
        {
            string _address = address.Trim();

            // If IPAddress is empty, reset to default
            if (_address.Length == 0) IPAddress = _defaultIpAddress;
            else IPAddress = _address;

            return IPAddress;
        }

    }

    public class Settings
    {
        private AppSettings appSettings;
        private readonly string fileName;

        public string IPAddress { get => appSettings.IPAddress; set => appSettings.SetIPAddress(appSettings.IPAddress = value); }
        public int Port { get => appSettings.Port; set => appSettings.Port = value; }
        public bool RunOnStartup { get => appSettings.RunOnStartup; set => appSettings.RunOnStartup = value; }
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
                // Get all text from our file, Base64 decode it to it's JSON string
                // Then deseralize that JSON string into an AppSettings object
                appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(fileName).Base64Decode());
            }
            catch
            {
                appSettings = new AppSettings();
                SetStartup(appSettings.RunOnStartup);
            }

            // If no accounts exist, add the default
            if (appSettings.Accounts.Count == 0)
                AddAccount("user", "password");
        }

        public void Save()
        {
            // Seralize appSettings class into JSON string
            // Then Base64 encode that string an save it to our file
            File.WriteAllText(fileName, JsonConvert.SerializeObject(appSettings).Base64Encode());
            SetStartup(appSettings.RunOnStartup);
        }

        public bool AddAccount(string userName, string password)
        {
            // Make sure the userName doesn't already exist
            if (GetAccountIndex(userName) != -1) return false;

            Accounts.Add(new Account(userName, password.Base64Encode()));
            Save();
            return true;
        }

        public bool RemoveAccount(string userName)
        {
            int index = GetAccountIndex(userName);

            // Make sure the userName exist
            if (index == -1) return false;

            Accounts.RemoveAt(index);
            return true;
        }

        public Account EditAccount(string userName, string newUsername, string newPassword)
        {
            Account account = GetAccount(userName);

            // Make sure an account was found
            if (account == null) return null;

            account.UserName = newUsername;
            if (!account.Password.Equals(newPassword))
                account.Password = newPassword.Base64Encode();
            return account;
        }

        public bool IsValidAccountLogin(string userName, string password)
        {
            Account account = GetAccount(userName);

            // Make sure an account was found
            if (account == null) return false;

            // Return if the password matches
            return (account.Password.Equals(password.Base64Encode()));
        }

        public int GetAccountIndex(string userName)
        {
            string _userName = userName.ToLower().Trim();
            return Accounts.FindIndex(a => a.LowerName.Equals(_userName));
        }

        public Account GetAccount(string userName)
        {
            string _userName = userName.ToLower().Trim();
            return Accounts.FirstOrDefault(a => a.LowerName.Equals(_userName));
        }

        private void SetStartup(bool addKey)
        {
            try
            {
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                if (addKey)
                    registry.SetValue(AppName, Application.ExecutablePath);
                else
                    registry.DeleteValue(AppName, false);
            } catch { }
        }


    }
}
