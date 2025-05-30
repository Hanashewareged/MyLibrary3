using System;
using System.Configuration;// Needed for ConfigurationManager (indirectly via DatabaseHelper)
using System.Data.SqlClient; // Changed from System.Data.SQLite
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mylibrary3
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Set password character for security
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          // Exits the entire application
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(this, new EventArgs());
                e.Handled = true; // Prevent the 'Enter' key from making a 'ding' sound
                e.SuppressKeyPress = true; // Suppress further key processing
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Any initialization logic for the login form on load
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Trim whitespace
            string password = txtPassword.Text;

            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Authenticate against the Users table using DatabaseHelper
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password;";
            SqlParameter[] parameters = new SqlParameter[] // Changed from SQLiteParameter
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            try
            {
                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                int userCount = Convert.ToInt32(result);

                if (userCount > 0)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Assuming 'mainForm' is your main application window
                    // Make sure 'mainForm' (or whatever your main form is named) exists in your project.
                    mainForm mainForm = new mainForm();
                    this.Hide(); // Hide the login form
                    mainForm.Show(); // Show the main application form
                    // Optional: Close the login form when the main form closes
                    mainForm.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear(); // Clear password for security
                    txtUsername.Focus(); // Set focus back to username
                }
            }
            catch (Exception ex)
            {
                // DatabaseHelper already shows a MessageBox for SQL exceptions,
                // but this catch can handle other unexpected errors during login process.
                MessageBox.Show("An unexpected error occurred during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Login Error: " + ex.ToString()); // Log full exception details
            }

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
