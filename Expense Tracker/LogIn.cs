using DataAccessLibrary.Data;
using DevExpress.Utils.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class LogIn : Form
    {
        private readonly IUserServices _user;
        public LogIn(IUserServices user)
        {
            InitializeComponent();
            _user = user;
        }
        private void showPasswordCheackBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPasswordCheackBox.Checked)
            {
                passwordTextBox.PasswordChar = '\0';
            }
            else
            {
                passwordTextBox.PasswordChar = '*';
            }
        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register(_user);
            register.ShowDialog();
        }
        private void loginButton_Click(object sender, EventArgs e)
        {

            List<string> emptyFields = new List<string>();
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                emptyFields.Add("User Name");
                userNameTextBox.Focus();
            }
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                emptyFields.Add("Password Name");
                passwordTextBox.Focus();
            }


            if (emptyFields.Count > 0)
            {
                string message = "Please enter the following fields:\n\n";
                message += string.Join("\n", emptyFields);
                MessageBox.Show(message, "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string userName = userNameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                bool logInSuccess = _user.AuthenticateUser(userName, password);

                if (logInSuccess)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login failed. The username or password is incorrect.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    userNameTextBox.Clear();
                    passwordTextBox.Clear();
                    userNameTextBox.Focus();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred during login. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
            }


        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            userNameTextBox.Text = "";
            passwordTextBox.Text = "";
        }
    }
}
