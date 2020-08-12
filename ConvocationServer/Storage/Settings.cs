﻿using ConvocationServer.Extensions;
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
        private readonly string _defaultIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();

        public string IPAddress { get; set; }
        public int Port { get; set; }
        public List<Account> Accounts { get; set; }

        public AppSettings()
        {
            // Set Defaults
            IPAddress = _defaultIpAddress;
            Port = 8181;
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
                appSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(fileName).Base64Decode());
            }
            catch
            {
                appSettings = new AppSettings();
            }
        }

        public void Save()
        {
            // Seralize appSettings class into JSON string
            // Then Base64 encode that string an save it to our file
            File.WriteAllText(fileName, JsonSerializer.Serialize(appSettings).Base64Encode());
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
