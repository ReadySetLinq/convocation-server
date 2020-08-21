using ConvocationServer.Extensions;
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
            selectedAccount = null;

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

        private void CtxMenuStripUser_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If no account is selected, cancel the menu from opening
            if (lstUsers.SelectedIndex == -1)
                e.Cancel = true;
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            Settings storageSettings = _parent.StorageSettings;
            // Setup new account to add
            Account _newAccount = new Account($"user{storageSettings.Accounts.Count + 1}", "password");

            // Try to add the new account, if this fails return out
            if (!_parent.StorageSettings.AddAccount(_newAccount.UserName, _newAccount.Password)) return;

            int newIndex = lstUsers.Items.Add(_newAccount.UserName);

            if (newIndex == -1) return;
            lstUsers.SelectedIndex = newIndex;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // If no account is selected, return
            if (selectedAccount == null) return;

            // If the userName was removed, don't save
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("You cannot save with an empty userName", "Failed to Save!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                return;
            };

            // If the password was removed, don't save
            if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("You cannot save with an empty password", "Failed to Save!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                return;
            };

            selectedAccount = _parent.StorageSettings.EditAccount(selectedAccount.UserName, txtUserName.Text, txtPassword.Text);
            _parent.StorageSettings.Save();

            int index = lstUsers.SelectedIndex;
            if (index == -1) return;

            // Update the lstUsers item userName
            lstUsers.Items[index] = txtUserName.Text.Trim();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reload the current selected user
            SelectUser();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Only continue if a selected account is set
            if (selectedAccount == null) return;

            DialogResult result = MessageBox.Show("Are you sure?\nThis action cannot be undone!", 
                                $"Remove: {selectedAccount.UserName}",
                                MessageBoxButtons.YesNo,  MessageBoxIcon.Warning);

            // If the user selected no, return
            if (result == DialogResult.No) return;

            _parent.StorageSettings.RemoveAccount(selectedAccount.UserName);
            _parent.StorageSettings.Save();

            // Get current selected index
            int index = lstUsers.SelectedIndex;
            lstUsers.Items.RemoveAt(index);

            // If  no other items exist, don't set selected again
            if (lstUsers.Items.Count == 0)
            {
                ResetSelectedUser();
                EnableSelectedUser(false);
                return;
            }

            if (index > 0)
                lstUsers.SelectedIndex = index - 1;
            else
                lstUsers.SelectedIndex = index;
        }

        private void UpdateUsers(bool resetSelected = true)
        {
            Settings storageSettings = _parent.StorageSettings;

            // Reset data
            ResetSelectedUser();
            EnableSelectedUser(false);
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
                ResetSelectedUser();
                EnableSelectedUser(false);
                return;
            }

            // Try and get an account based off the selected item
            selectedAccount = _parent.StorageSettings.GetAccount(lstUsers.SelectedItem.ToString());

            // If account was not found, do nothing
            if (selectedAccount == null)
            {
                ResetSelectedUser();
                EnableSelectedUser(false);
                return;
            }

            // Fill in Selected User fields
            txtUserName.Text = selectedAccount.UserName;
            // Make sure the password is only added to a text box as astricts
            txtPassword.Text = selectedAccount.Password;

            EnableSelectedUser(true);
        }

        private void ResetSelectedUser()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            selectedAccount = null;
            lstUsers.SelectedIndex = -1;
        }

        private void EnableSelectedUser(bool enable = false)
        {
            txtUserName.Enabled = enable;
            txtPassword.Enabled = enable;
            btnSave.Enabled = enable;
            btnReset.Enabled = enable;
        }
    }
}
