using System;
using System.Data;
using System.Data.SqlClient; // Ensure this is present for SqlParameter
using System.Windows.Forms;

namespace Mylibrary3
{
    public partial class BookForm : Form
    {
        public int BookID { get; private set; } // Property to hold the BookID (0 for new book)

        /// <summary>
        /// Constructor for the BookForm.
        /// </summary>
        /// <param name="bookId">The ID of the book to edit. Pass 0 for a new book.</param>
        public BookForm(int bookId = 0)
        {
            InitializeComponent();
            BookID = bookId;

            // --- THIS IS THE CODE SNIPPET YOU PROVIDED AND REQUESTED ---
            if (BookID > 0)
            {
                this.Text = "Edit Book";
                LoadBookDetails(); // Load existing book data if editing
            }
            else
            {
                this.Text = "Add New Book";
                // Set default values for new book if desired
                numYear.Value = DateTime.Now.Year;
                numTotalCopies.Value = 1;
                numAvailableCopies.Value = 1;
            }
            // --- END OF CODE SNIPPET ---

            // Attach event handlers for buttons
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// Loads the details of an existing book from the database into the form controls.
        /// This is called when BookID > 0 (i.e., in edit mode).
        /// </summary>
        private void LoadBookDetails()
        {
            string query = "SELECT Title, Author, Year, TotalCopies, AvailableCopies FROM Books WHERE BookID = @bookId;";
            SqlParameter[] parameters = { new SqlParameter("@bookId", BookID) };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtTitle.Text = row["Title"].ToString();
                txtAuthor.Text = row["Author"].ToString();
                numYear.Value = Convert.ToInt32(row["Year"]);
                numTotalCopies.Value = Convert.ToInt32(row["TotalCopies"]);
                numAvailableCopies.Value = Convert.ToInt32(row["AvailableCopies"]);
            }
            else
            {
                MessageBox.Show("Could not load book details. Book may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel; // Close form if data can't be loaded
                this.Close();
            }
        }

        /// <summary>
        /// Handles the click event for the 'Save' button.
        /// Performs input validation and saves book details to the database (INSERT or UPDATE).
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- Input Validation ---
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Book Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Author cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthor.Focus();
                return;
            }

            // Validate Year range
            if (numYear.Value < 1000 || numYear.Value > DateTime.Now.Year + 1) // Example: Year between 1000 and current year + 1
            {
                MessageBox.Show($"Please enter a valid year between 1000 and {DateTime.Now.Year + 1}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numYear.Focus();
                return;
            }

            // Validate copies
            if (numTotalCopies.Value < 0)
            {
                MessageBox.Show("Total Copies cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numTotalCopies.Focus();
                return;
            }
            if (numAvailableCopies.Value < 0)
            {
                MessageBox.Show("Available Copies cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAvailableCopies.Focus();
                return;
            }
            if (numAvailableCopies.Value > numTotalCopies.Value)
            {
                MessageBox.Show("Available Copies cannot exceed Total Copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAvailableCopies.Focus();
                return;
            }

            // If editing, ensure AvailableCopies doesn't drop below currently issued count
            if (BookID > 0)
            {
                string checkIssuedQuery = "SELECT (TotalCopies - AvailableCopies) FROM Books WHERE BookID = @bookId;";
                SqlParameter[] checkIssuedParams = { new SqlParameter("@bookId", BookID) };
                object currentlyIssuedObj = DatabaseHelper.ExecuteScalar(checkIssuedQuery, checkIssuedParams);
                int currentlyIssued = (currentlyIssuedObj != null) ? Convert.ToInt32(currentlyIssuedObj) : 0;

                // Ensure the new TotalCopies isn't less than the number of books currently issued
                if (numTotalCopies.Value < currentlyIssued)
                {
                    MessageBox.Show($"Cannot set Total Copies below the currently issued count ({currentlyIssued}). Please increase Total Copies or return some books first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numTotalCopies.Focus();
                    return;
                }

                // When updating, AvailableCopies must be at least (new TotalCopies - currently issued)
                // This prevents reducing available copies more than physically possible if books are out.
                if (numAvailableCopies.Value < (numTotalCopies.Value - currentlyIssued))
                {
                    // If the user tries to set AvailableCopies too low, adjust it to the minimum possible
                    numAvailableCopies.Value = numTotalCopies.Value - currentlyIssued;
                    MessageBox.Show($"Available Copies has been adjusted to {numAvailableCopies.Value} to account for currently issued books.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // If you prefer to prevent the save and require user correction:
                    // MessageBox.Show($"Available Copies must be at least {numTotalCopies.Value - currentlyIssued} based on Total Copies and currently issued books.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // numAvailableCopies.Focus();
                    // return;
                }
            }


            // --- Data Preparation ---
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            int year = (int)numYear.Value;
            int totalCopies = (int)numTotalCopies.Value;
            int availableCopies = (int)numAvailableCopies.Value;

            string query;
            SqlParameter[] parameters;
            int rowsAffected = -1;

            if (BookID == 0) // Add New Book
            {
                query = "INSERT INTO Books (Title, Author, Year, TotalCopies, AvailableCopies) VALUES (@title, @author, @year, @totalCopies, @availableCopies);";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@title", title),
                    new SqlParameter("@author", author),
                    new SqlParameter("@year", year),
                    new SqlParameter("@totalCopies", totalCopies),
                    new SqlParameter("@availableCopies", availableCopies)
                };
                rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            else // Edit Existing Book
            {
                query = "UPDATE Books SET Title = @title, Author = @author, Year = @year, TotalCopies = @totalCopies, AvailableCopies = @availableCopies WHERE BookID = @bookId;";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@title", title),
                    new SqlParameter("@author", author),
                    new SqlParameter("@year", year),
                    new SqlParameter("@totalCopies", totalCopies),
                    new SqlParameter("@availableCopies", availableCopies),
                    new SqlParameter("@bookId", BookID)
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
                MessageBox.Show("Failed to save book details. Please try again.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
