using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net.Mail; // For email validation

namespace Mylibrary3
{
    public partial class BorrowerForm : Form
    {
        public int BorrowerID { get; private set; } // 0 for new borrower

        public BorrowerForm(int borrowerId = 0)
        {
            InitializeComponent();
            BorrowerID = borrowerId;

            if (BorrowerID > 0)
            {
                this.Text = "Edit Borrower";
                LoadBorrowerDetails();
            }
            else
            {
                this.Text = "Add New Borrower";
                // Optionally set default values for new borrower fields
            }

            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// Loads the details of an existing borrower from the database into the form controls.
        /// This is called when BorrowerID > 0 (i.e., in edit mode).
        /// </summary>
        private void LoadBorrowerDetails()
        {
            string query = "SELECT Name, Email, Phone FROM Borrowers WHERE BorrowerID = @borrowerId;";
            SqlParameter[] parameters = { new SqlParameter("@borrowerId", BorrowerID) };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtName.Text = row["Name"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhone.Text = row["Phone"].ToString();
            }
            else
            {
                MessageBox.Show("Could not load borrower details. Borrower may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel; // Close form if data can't be loaded
                this.Close();
            }
        }

        /// <summary>
        /// Handles the click event for the 'Save' button.
        /// Performs input validation and saves borrower details to the database (INSERT or UPDATE).
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- Input Validation ---
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Borrower Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Email format validation
            try
            {
                MailAddress mail = new MailAddress(txtEmail.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // --- Data Preparation ---
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim(); // Phone can be empty per schema

            string query;
            SqlParameter[] parameters;
            int rowsAffected = -1;

            if (BorrowerID == 0) // Add New Borrower
            {
                query = "INSERT INTO Borrowers (Name, Email, Phone) VALUES (@name, @email, @phone);";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@phone", phone)
                };
                rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            else // Edit Existing Borrower
            {
                query = "UPDATE Borrowers SET Name = @name, Email = @email, Phone = @phone WHERE BorrowerID = @borrowerId;";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@phone", phone),
                    new SqlParameter("@borrowerId", BorrowerID)
                };
                rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            }

            if (rowsAffected > 0)
            {
                this.DialogResult = DialogResult.OK; // Indicate success to the calling form (MainForm)
                this.Close();
            }
            else
            {
                // DatabaseHelper already shows a message for SQL exceptions,
                // but this handles cases where 0 rows were affected for other reasons.
                MessageBox.Show("Failed to save borrower details. Please try again.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Cancel' button.
        /// Closes the form without saving changes.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }
    }
}
