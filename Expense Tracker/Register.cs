using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using DevExpress.Utils.IoC;
using Microsoft.Extensions.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Expense_Tracker
{
    public partial class Register : Form
    {
        private readonly IUserServices _user;
        public Register(IUserServices user)
        {
            InitializeComponent();
            _user = user;
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn(_user);
            logIn.ShowDialog();
        }
        private void showPasswordCheackBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPasswordCheackBox.Checked)
            {
                passwordTextBox.PasswordChar = '\0';
                conformPasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                passwordTextBox.PasswordChar = '*';
                conformPasswordTextBox.PasswordChar = '*';
            }
        }
        private void registerButton_Click(object sender, EventArgs e)
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
            if (string.IsNullOrWhiteSpace(conformPasswordTextBox.Text))
            {
                emptyFields.Add("Confirm Password");
                conformPasswordTextBox.Focus();
            }

            if (emptyFields.Count > 0)
            {
                string message = "Please enter the following fields:\n\n";
                message += string.Join("\n", emptyFields);
                MessageBox.Show(message, "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (passwordTextBox.Text != conformPasswordTextBox.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match. Please re-enter them.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordTextBox.Clear();
                conformPasswordTextBox.Clear();
                passwordTextBox.Focus();
                return;
            }

            string userName = userNameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                bool registrationSuccess = _user.RegisterUser(userName, password);

                if (registrationSuccess)
                {
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    LogIn logIn = new LogIn(_user);
                    logIn.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Registration failed. The username may already be in use or there was an error during registration.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    userNameTextBox.Clear();
                    passwordTextBox.Clear();
                    conformPasswordTextBox.Clear();
                    userNameTextBox.Focus();
                }
            }
            catch (RegistrationFailedException ex)
            {
                MessageBox.Show(ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred during registration. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            userNameTextBox.Text = "";
            passwordTextBox.Text = "";
            conformPasswordTextBox.Text = "";
        }
    }
}