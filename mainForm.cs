using System;
using System.Data;
using System.Data.SqlClient; // Ensure this is present
using System.Windows.Forms;
using System.Collections.Generic; // Needed for List<SqlParameter>

namespace Mylibrary3
{
    public partial class mainForm : Form // Assuming your main form is named 'mainForm'
    {
        public mainForm()
        {
            InitializeComponent();
            // Attach event handlers for the form itself
            this.Load += new EventHandler(MainForm_Load);

            // Attach event handlers for tab control to refresh data on tab switch
            this.tabControlMain.SelectedIndexChanged += new EventHandler(TabControlMain_SelectedIndexChanged);

            // Book Management Button Event Handlers (from previous iteration)
            this.btnAddBook.Click += new EventHandler(btnAddBook_Click);
            this.btnEditBook.Click += new EventHandler(btnEditBook_Click);
            this.btnDeleteBook.Click += new EventHandler(btnDeleteBook_Click);

            // Borrower Management Button Event Handlers (NEW)
            this.btnAddBorrower.Click += new EventHandler(btnAddBorrower_Click);
            this.btnEditBorrower.Click += new EventHandler(btnEditBorrower_Click);
            this.btnDeleteBorrower.Click += new EventHandler(btnDeleteBorrower_Click);
            this.btnIssueBook.Click += new EventHandler(btnIssueBook_Click);
            this.btnReturnBook.Click += new EventHandler(btnReturnBook_Click);

            // Optional: Filter button handler (if you add filter controls to Books tab)
            // this.btnApplyFilter.Click += new EventHandler(btnApplyFilter_Click);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load initial data for both tabs when the main form loads
            LoadBooks();
            LoadBorrowers();
            LoadIssuedBooks(); // Load initially
        }

        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Refresh data when switching tabs to ensure up-to-date information
            if (tabControlMain.SelectedTab == tabPageBooks)
            {
                LoadBooks();
            }
            else if (tabControlMain.SelectedTab == tabPageBorrowers)
            {
                LoadBorrowers();
                LoadIssuedBooks(); // Also refresh issued books when on this tab
            }
        }

        // --- Book Management Methods (from previous iteration) ---

        /// <summary>
        /// Loads book data from the database into the dgvBooks DataGridView.
        /// </summary>
        private void LoadBooks()
        {
            string query = "SELECT BookID, Title, Author, Year, TotalCopies, AvailableCopies FROM Books;";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null)
            {
                dgvBooks.DataSource = dt;
                dgvBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                if (dgvBooks.Columns.Contains("BookID"))
                {
                    dgvBooks.Columns["BookID"].DisplayIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Failed to load books data.", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Add Book' button.
        /// Opens a form to enter new book details.
        /// </summary>
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            // Assuming you have a BookForm for adding/editing books
            BookForm bookForm = new BookForm(0); // 0 indicates a new book
            if (bookForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks(); // Refresh the DataGridView after a successful add
                MessageBox.Show("New book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Edit Book' button.
        /// Opens a form to edit details of the selected book.
        /// </summary>
        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
            BookForm bookForm = new BookForm(bookId); // Pass the selected BookID to the form
            if (bookForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks(); // Refresh the DataGridView after a successful edit
                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Delete Book' button.
        /// Deletes the selected book after confirmation.
        /// </summary>
        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookID"].Value);
            string bookTitle = dgvBooks.SelectedRows[0].Cells["Title"].Value.ToString();

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the book '{bookTitle}'?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                // IMPORTANT: Check for existing issued books before deleting
                string checkIssuedQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BookID = @bookId AND Returned = 0;"; // Check for NOT returned
                SqlParameter[] checkIssuedParams = { new SqlParameter("@bookId", bookId) };
                object issuedCountObj = DatabaseHelper.ExecuteScalar(checkIssuedQuery, checkIssuedParams);
                int issuedCount = (issuedCountObj != null) ? Convert.ToInt32(issuedCountObj) : 0;

                if (issuedCount > 0)
                {
                    MessageBox.Show("Cannot delete book: There are outstanding issues for this book. Please ensure all copies are returned first.", "Deletion Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string deleteQuery = "DELETE FROM Books WHERE BookID = @bookId;";
                SqlParameter[] deleteParams = { new SqlParameter("@bookId", bookId) };

                if (DatabaseHelper.ExecuteNonQuery(deleteQuery, deleteParams) > 0)
                {
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooks(); // Refresh the DataGridView
                }
                else
                {
                    MessageBox.Show("Failed to delete book.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Borrower Management Methods (NEW) ---

        /// <summary>
        /// Loads borrower data from the database into the dgvBorrowers DataGridView.
        /// </summary>
        private void LoadBorrowers()
        {
            string query = "SELECT BorrowerID, Name, Email, Phone FROM Borrowers;";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null)
            {
                dgvBorrowers.DataSource = dt;
                dgvBorrowers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                if (dgvBorrowers.Columns.Contains("BorrowerID"))
                {
                    dgvBorrowers.Columns["BorrowerID"].DisplayIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Failed to load borrowers data.", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads currently issued book data from the database into the dgvIssuedBooks DataGridView.
        /// Filters for books that have not yet been returned (Returned = 0).
        /// </summary>
        private void LoadIssuedBooks()
        {
            string query = @"
                SELECT
                    ib.IssueID,
                    b.Title AS BookTitle,
                    br.Name AS BorrowerName,
                    ib.IssueDate,
                    ib.DueDate
                FROM IssuedBooks ib
                JOIN Books b ON ib.BookID = b.BookID
                JOIN Borrowers br ON ib.BorrowerID = br.BorrowerID
                WHERE ib.Returned = 0; -- Only show currently issued books
            ";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt != null)
            {
                dgvIssuedBooks.DataSource = dt;
                dgvIssuedBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            else
            {
                MessageBox.Show("Failed to load issued books data.", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Add Borrower' button.
        /// Opens a form to enter new borrower details.
        /// </summary>
        private void btnAddBorrower_Click(object sender, EventArgs e)
        {
            // Assuming you have a BorrowerForm for adding/editing borrowers
            BorrowerForm borrowerForm = new BorrowerForm(0); // 0 indicates a new borrower
            if (borrowerForm.ShowDialog() == DialogResult.OK)
            {
                LoadBorrowers(); // Refresh the DataGridView after a successful add
                MessageBox.Show("New borrower added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Edit Borrower' button.
        /// Opens a form to edit details of the selected borrower.
        /// </summary>
        private void btnEditBorrower_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowerId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value);
            BorrowerForm borrowerForm = new BorrowerForm(borrowerId); // Pass the selected BorrowerID
            if (borrowerForm.ShowDialog() == DialogResult.OK)
            {
                LoadBorrowers(); // Refresh the DataGridView after a successful edit
                MessageBox.Show("Borrower updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Delete Borrower' button.
        /// Deletes the selected borrower after confirmation.
        /// </summary>
        private void btnDeleteBorrower_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowerId = Convert.ToInt32(dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value);
            string borrowerName = dgvBorrowers.SelectedRows[0].Cells["Name"].Value.ToString();

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the borrower '{borrowerName}'?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                // IMPORTANT: Check for outstanding issued books by this borrower
                string checkIssuedQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BorrowerID = @borrowerId AND Returned = 0;";
                SqlParameter[] checkIssuedParams = { new SqlParameter("@borrowerId", borrowerId) };
                object issuedCountObj = DatabaseHelper.ExecuteScalar(checkIssuedQuery, checkIssuedParams);
                int issuedCount = (issuedCountObj != null) ? Convert.ToInt32(issuedCountObj) : 0;

                if (issuedCount > 0)
                {
                    MessageBox.Show("Cannot delete borrower: This borrower has outstanding issued books. Please ensure all books are returned first.", "Deletion Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string deleteQuery = "DELETE FROM Borrowers WHERE BorrowerID = @borrowerId;";
                SqlParameter[] deleteParams = { new SqlParameter("@borrowerId", borrowerId) };

                if (DatabaseHelper.ExecuteNonQuery(deleteQuery, deleteParams) > 0)
                {
                    MessageBox.Show("Borrower deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowers(); // Refresh the DataGridView
                }
                else
                {
                    MessageBox.Show("Failed to delete borrower.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the click event for the 'Issue Book' button.
        /// Opens a form to select a borrower and a book to issue.
        /// </summary>
        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            // Assuming you have an IssueBookForm
            IssueBookForm issueForm = new IssueBookForm();
            if (issueForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();        // Refresh books count (AvailableCopies)
                LoadIssuedBooks();  // Refresh issued books list
                MessageBox.Show("Book issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Return Book' button.
        /// Marks the selected issued book as returned and increments available copies.
        /// </summary>
        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (dgvIssuedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an issued book to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvIssuedBooks.SelectedRows[0];
            int issueId = Convert.ToInt32(selectedRow.Cells["IssueID"].Value);
            string bookTitle = selectedRow.Cells["BookTitle"].Value.ToString();
            string borrowerName = selectedRow.Cells["BorrowerName"].Value.ToString();

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to return '{bookTitle}' by '{borrowerName}'?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                // Get the BookID associated with this IssueID
                string getBookIdQuery = "SELECT BookID FROM IssuedBooks WHERE IssueID = @issueId;";
                SqlParameter[] getBookIdParams = { new SqlParameter("@issueId", issueId) };
                object bookIdObj = DatabaseHelper.ExecuteScalar(getBookIdQuery, getBookIdParams);

                if (bookIdObj == null)
                {
                    MessageBox.Show("Could not find associated book for this issue record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int bookId = Convert.ToInt32(bookIdObj);

                // Start a transaction for atomicity (recommended for multi-step updates)
                // For simplicity here, we'll do two separate calls, but a transaction is safer.
                // If you implement transactions in DatabaseHelper, use them here.

                // 1. Mark the book as returned in IssuedBooks table
                string updateIssuedQuery = "UPDATE IssuedBooks SET Returned = 1 WHERE IssueID = @issueId;";
                SqlParameter[] updateIssuedParams = { new SqlParameter("@issueId", issueId) };
                int rowsAffectedIssued = DatabaseHelper.ExecuteNonQuery(updateIssuedQuery, updateIssuedParams);

                if (rowsAffectedIssued > 0)
                {
                    // 2. Increment AvailableCopies in Books table
                    string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @bookId;";
                    SqlParameter[] updateBookParams = { new SqlParameter("@bookId", bookId) };
                    int rowsAffectedBook = DatabaseHelper.ExecuteNonQuery(updateBookQuery, updateBookParams);

                    if (rowsAffectedBook > 0)
                    {
                        MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadIssuedBooks(); // Refresh issued books list
                        LoadBooks();       // Refresh books list (available copies changed)
                    }
                    else
                    {
                        MessageBox.Show("Failed to update book copies after return. Please check database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // In a real app, you might try to rollback the 'Returned = 1' if this fails.
                    }
                }
                else
                {
                    MessageBox.Show("Failed to mark book as returned in records. Please check database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Reports/Filtering (Bonus) ---

        /// <summary>
        /// Handles the click event for the 'Apply Filter' button on the Books tab.
        /// Filters the books displayed based on author and year range.
        /// </summary>
        /*
        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            string query = "SELECT BookID, Title, Author, Year, TotalCopies, AvailableCopies FROM Books WHERE 1=1 ";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(txtFilterAuthor.Text))
            {
                query += " AND Author LIKE @authorFilter ";
                parameters.Add(new SqlParameter("@authorFilter", "%" + txtFilterAuthor.Text.Trim() + "%"));
            }

            if (int.TryParse(txtFilterYearStart.Text, out int yearStart))
            {
                query += " AND Year >= @yearStart ";
                parameters.Add(new SqlParameter("@yearStart", yearStart));
            }

            if (int.TryParse(txtFilterYearEnd.Text, out int yearEnd))
            {
                query += " AND Year <= @yearEnd ";
                parameters.Add(new SqlParameter("@yearEnd", yearEnd));
            }

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            if (dt != null)
            {
                dgvBooks.DataSource = dt;
                dgvBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }
        */

        /// <summary>
        /// Handles the click event for the 'Overdue Report' button on the Borrowers tab.
        /// Generates a report of currently overdue books.
        /// </summary>
        /*
        private void btnOverdueReport_Click(object sender, EventArgs e)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string query = $@"
                SELECT
                    b.Title AS BookTitle,
                    br.Name AS BorrowerName,
                    ib.IssueDate,
                    ib.DueDate
                FROM IssuedBooks ib
                JOIN Books b ON ib.BookID = b.BookID
                JOIN Borrowers br ON ib.BorrowerID = br.BorrowerID
                WHERE ib.Returned = 0 AND ib.DueDate < @today;
            ";
            SqlParameter[] parameters = { new SqlParameter("@today", today) };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                string report = "--- Overdue Books ---\n\n";
                foreach (DataRow row in dt.Rows)
                {
                    report += $"Book: {row["BookTitle"]}, Borrower: {row["BorrowerName"]}, Due Date: {Convert.ToDateTime(row["DueDate"]).ToShortDateString()}\n";
                }
                MessageBox.Show(report, "Overdue Books Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No overdue books found!", "Overdue Books Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        */
    }
}
