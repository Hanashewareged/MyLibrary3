using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mylibrary3
{
    public partial class IssueBookForm : Form
    {
        public IssueBookForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(IssueBookForm_Load);
            this.btnIssue.Click += new EventHandler(btnIssue_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void IssueBookForm_Load(object sender, EventArgs e)
        {
            LoadBorrowersIntoComboBox();
            LoadAvailableBooksIntoComboBox();

            // Set default dates
            dtpIssueDate.Value = DateTime.Today;
            dtpDueDate.Value = DateTime.Today.AddDays(14); // Default due in 14 days
        }

        /// <summary>
        /// Populates the cmbBorrowers ComboBox with borrower names and IDs.
        /// </summary>
        private void LoadBorrowersIntoComboBox()
        {
            string query = "SELECT BorrowerID, Name FROM Borrowers ORDER BY Name;";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null)
            {
                cmbBorrowers.DataSource = dt;
                cmbBorrowers.DisplayMember = "Name";
                cmbBorrowers.ValueMember = "BorrowerID";
                cmbBorrowers.SelectedIndex = -1; // No selection by default
            }
            else
            {
                MessageBox.Show("Failed to load borrowers for selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populates the cmbBooks ComboBox with available book titles and IDs.
        /// </summary>
        private void LoadAvailableBooksIntoComboBox()
        {
            string query = "SELECT BookID, Title FROM Books WHERE AvailableCopies > 0 ORDER BY Title;";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null)
            {
                cmbBooks.DataSource = dt;
                cmbBooks.DisplayMember = "Title";
                cmbBooks.ValueMember = "BookID";
                cmbBooks.SelectedIndex = -1; // No selection by default
            }
            else
            {
                MessageBox.Show("Failed to load available books for selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Issue' button.
        /// Records the book issue and decrements available copies.
        /// </summary>
        private void btnIssue_Click(object sender, EventArgs e)
        {
            // --- Input Validation ---
            if (cmbBorrowers.SelectedValue == null)
            {
                MessageBox.Show("Please select a borrower.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbBooks.SelectedValue == null)
            {
                MessageBox.Show("Please select a book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpDueDate.Value < dtpIssueDate.Value)
            {
                MessageBox.Show("Due Date cannot be before Issue Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowerId = (int)cmbBorrowers.SelectedValue;
            int bookId = (int)cmbBooks.SelectedValue;
            string issueDate = dtpIssueDate.Value.ToString("yyyy-MM-dd");
            string dueDate = dtpDueDate.Value.ToString("yyyy-MM-dd");

            // Check if book is still available (race condition check)
            string checkCopiesQuery = "SELECT AvailableCopies FROM Books WHERE BookID = @bookId;";
            SqlParameter[] checkParams = { new SqlParameter("@bookId", bookId) };
            object result = DatabaseHelper.ExecuteScalar(checkCopiesQuery, checkParams);
            int availableCopies = (result != null) ? Convert.ToInt32(result) : 0;

            if (availableCopies <= 0)
            {
                MessageBox.Show("No available copies for this book. Please select another book.", "Issue Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadAvailableBooksIntoComboBox(); // Refresh list in case it changed
                return;
            }

            // --- Database Operations (ideally within a transaction) ---
            // For simplicity, using two separate non-query calls.
            // A proper transaction would ensure both succeed or both fail.

            // 1. Decrement AvailableCopies in Books table
            string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @bookId;";
            SqlParameter[] updateBookParams = { new SqlParameter("@bookId", bookId) };
            int rowsAffectedBook = DatabaseHelper.ExecuteNonQuery(updateBookQuery, updateBookParams);

            if (rowsAffectedBook > 0)
            {
                // 2. Insert record into IssuedBooks table
                string insertIssueQuery = "INSERT INTO IssuedBooks (BookID, BorrowerID, IssueDate, DueDate, Returned) VALUES (@bookId, @borrowerId, @issueDate, @dueDate, 0);";
                SqlParameter[] insertIssueParams = new SqlParameter[]
                {
                    new SqlParameter("@bookId", bookId),
                    new SqlParameter("@borrowerId", borrowerId),
                    new SqlParameter("@issueDate", issueDate),
                    new SqlParameter("@dueDate", dueDate)
                };
                int rowsAffectedIssue = DatabaseHelper.ExecuteNonQuery(insertIssueQuery, insertIssueParams);

                if (rowsAffectedIssue > 0)
                {
                    this.DialogResult = DialogResult.OK; // Indicate success to parent form
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to record issue. Rolling back book update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Rollback the book update if issue record failed (manual rollback)
                    DatabaseHelper.ExecuteNonQuery("UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @bookId;", updateBookParams);
                }
            }
            else
            {
                MessageBox.Show("Failed to update book copies. Book not issued.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
