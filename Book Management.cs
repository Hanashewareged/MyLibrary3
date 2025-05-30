using System;
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
    public partial class BookManagment : UserControl
    {
        public BookManagment()
        {
            InitializeComponent();
        }

        // Logic for Add Book, Edit Book, Delete Book buttons
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Book logic goes here!");
            // Implement functionality to open a new form for adding a book
            // or add a new row to the DataGridView directly.
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Edit Book logic goes here!");
            // Implement functionality to edit the selected book in the DataGridView.
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected book(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Iterate in reverse to avoid issues when removing rows while iterating
                    for (int i = dataGridViewBooks.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        dataGridViewBooks.Rows.RemoveAt(dataGridViewBooks.SelectedRows[i].Index);
                    }
                    MessageBox.Show("Book(s) deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Populate DataGridView with dummy data on load
        private void BookManagment_Load(object sender, EventArgs e)
        {
            // The columns are already defined in the Designer.cs.
            // This part is for adding initial data.
            dataGridViewBooks.Rows.Add("B001", "The Hitchhiker's Guide to the Galaxy", "Douglas Adams", "1979");
            dataGridViewBooks.Rows.Add("B002", "Pride and Prejudice", "Jane Austen", "1813");
            dataGridViewBooks.Rows.Add("B003", "Dune", "Frank Herbert", "1965");
        }
    }
}

    