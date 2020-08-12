﻿using ConvocationServer.Extensions;
using ConvocationServer.Storage;
using System;
using System.Data;
using System.Windows.Forms;

namespace ConvocationServer.Forms
{
    public partial class FrmUsers : Form
    {
        private readonly FrmServer _parent;
        private Account selectedAccount;

        public FrmUsers(FrmServer parent)
        {
            InitializeComponent();

            _parent = parent;

            // Reset user list
            UpdateUsers();
        }

        private void FrmUsers_Shown(object sender, EventArgs e)
        {
            // Keep the user list updated
            UpdateUsers();

            EnableSelectedUser(false);
        }

        private void LstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load the selected user
            SelectUser();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // If no account is selected, return
            if (selectedAccount == null) return;

            // If the password was removed, don't save
            if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("You cannot save with an empty password", "Failed to Save!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                return;
            };

            Settings storageSettings = _parent.StorageSettings;
            storageSettings.EditAccountPassword(txtUserName.Text.Trim(), txtPassword.Text);
            storageSettings.Save();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reload the current selected user
            SelectUser();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void UpdateUsers()
        {
            Settings storageSettings = _parent.StorageSettings;

            // Reset data
            txtUserName.Text = "";
            txtPassword.Text = "";
            lstUsers.Items.Clear();

            foreach (Account account in storageSettings.Accounts)
            {
                // Skip any null accounts
                if (account == null) continue;

                lstUsers.Items.Add(account.UserName);
            }
        }

        private void SelectUser()
        {
            // If nothing is selected, return
            if (lstUsers.SelectedIndex == -1)
            {
                EnableSelectedUser(false);
                return;
            }

            // Try and get an account based off the selected item
            selectedAccount = _parent.StorageSettings.GetAccount(lstUsers.SelectedItem.ToString());

            // If account was not found, do nothing
            if (selectedAccount == null)
            {
                EnableSelectedUser(false);
                return;
            }

            // Fill in Selected User fields
            txtUserName.Text = selectedAccount.UserName;
            // Make sure the password is only added to a text box as astricts
            txtPassword.Text = selectedAccount.Password.AsAstricts();

            EnableSelectedUser(true);
        }

        private void EnableSelectedUser(bool enable = false)
        {
            txtPassword.Enabled = enable;
            btnSave.Enabled = enable;
            btnReset.Enabled = enable;
        }

    }
}
