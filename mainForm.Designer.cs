namespace Mylibrary3
{
    partial class mainForm // Note: 'partial' class - the other part is in MainForm.cs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBooks = new System.Windows.Forms.TabPage();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnEditBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.tabPageBorrowers = new System.Windows.Forms.TabPage();
            this.dgvBorrowers = new System.Windows.Forms.DataGridView();
            this.btnAddBorrower = new System.Windows.Forms.Button();
            this.btnEditBorrower = new System.Windows.Forms.Button();
            this.btnDeleteBorrower = new System.Windows.Forms.Button();
            this.btnIssueBook = new System.Windows.Forms.Button();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.dgvIssuedBooks = new System.Windows.Forms.DataGridView(); // Added for Issued Books display

            this.tabControlMain.SuspendLayout();
            this.tabPageBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.tabPageBorrowers.SuspendLayout(); // Added for Borrowers tab
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).BeginInit(); // Added for Borrowers tab
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).BeginInit(); // Added for Issued Books display
            this.SuspendLayout();
            //
            // tabControlMain
            //
            this.tabControlMain.Controls.Add(this.tabPageBooks);
            this.tabControlMain.Controls.Add(this.tabPageBorrowers);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 600); // Example size
            this.tabControlMain.TabIndex = 0;
            //
            // tabPageBooks
            //
            this.tabPageBooks.Controls.Add(this.dgvBooks);
            this.tabPageBooks.Controls.Add(this.btnAddBook);
            this.tabPageBooks.Controls.Add(this.btnEditBook);
            this.tabPageBooks.Controls.Add(this.btnDeleteBook);
            this.tabPageBooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageBooks.Name = "tabPageBooks";
            this.tabPageBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBooks.Size = new System.Drawing.Size(792, 574); // Example size
            this.tabPageBooks.TabIndex = 0;
            this.tabPageBooks.Text = "Book Management";
            this.tabPageBooks.UseVisualStyleBackColor = true;
            //
            // dgvBooks
            //
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(10, 10); // Example position
            this.dgvBooks.MultiSelect = false;
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(770, 480); // Example size
            this.dgvBooks.TabIndex = 0;
            //
            // btnAddBook
            //
            this.btnAddBook.Location = new System.Drawing.Point(10, 500); // Example position
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(100, 30); // Example size
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "Add Book";
            this.btnAddBook.UseVisualStyleBackColor = true;
            //
            // btnEditBook
            //
            this.btnEditBook.Location = new System.Drawing.Point(120, 500); // Example position
            this.btnEditBook.Name = "btnEditBook";
            this.btnEditBook.Size = new System.Drawing.Size(100, 30);
            this.btnEditBook.TabIndex = 2;
            this.btnEditBook.Text = "Edit Book";
            this.btnEditBook.UseVisualStyleBackColor = true;
            //
            // btnDeleteBook
            //
            this.btnDeleteBook.Location = new System.Drawing.Point(230, 500); // Example position
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteBook.TabIndex = 3;
            this.btnDeleteBook.Text = "Delete Book";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            //
            // tabPageBorrowers
            //
            this.tabPageBorrowers.Controls.Add(this.dgvIssuedBooks); // Added for Issued Books
            this.tabPageBorrowers.Controls.Add(this.btnReturnBook);
            this.tabPageBorrowers.Controls.Add(this.btnIssueBook);
            this.tabPageBorrowers.Controls.Add(this.btnDeleteBorrower);
            this.tabPageBorrowers.Controls.Add(this.btnEditBorrower);
            this.tabPageBorrowers.Controls.Add(this.btnAddBorrower);
            this.tabPageBorrowers.Controls.Add(this.dgvBorrowers);
            this.tabPageBorrowers.Location = new System.Drawing.Point(4, 22);
            this.tabPageBorrowers.Name = "tabPageBorrowers";
            this.tabPageBorrowers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBorrowers.Size = new System.Drawing.Size(792, 574); // Example size
            this.tabPageBorrowers.TabIndex = 1;
            this.tabPageBorrowers.Text = "Borrower Management";
            this.tabPageBorrowers.UseVisualStyleBackColor = true;
            //
            // dgvBorrowers
            //
            this.dgvBorrowers.AllowUserToAddRows = false;
            this.dgvBorrowers.AllowUserToDeleteRows = false;
            this.dgvBorrowers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBorrowers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowers.Location = new System.Drawing.Point(10, 10); // Example position
            this.dgvBorrowers.MultiSelect = false;
            this.dgvBorrowers.Name = "dgvBorrowers";
            this.dgvBorrowers.ReadOnly = true;
            this.dgvBorrowers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowers.Size = new System.Drawing.Size(550, 200); // Example size
            this.dgvBorrowers.TabIndex = 0;
            //
            // btnAddBorrower
            //
            this.btnAddBorrower.Location = new System.Drawing.Point(570, 10); // Example position, right side
            this.btnAddBorrower.Name = "btnAddBorrower";
            this.btnAddBorrower.Size = new System.Drawing.Size(120, 30);
            this.btnAddBorrower.TabIndex = 1;
            this.btnAddBorrower.Text = "Add Borrower";
            this.btnAddBorrower.UseVisualStyleBackColor = true;
            //
            // btnEditBorrower
            //
            this.btnEditBorrower.Location = new System.Drawing.Point(570, 50); // Example position
            this.btnEditBorrower.Name = "btnEditBorrower";
            this.btnEditBorrower.Size = new System.Drawing.Size(120, 30);
            this.btnEditBorrower.TabIndex = 2;
            this.btnEditBorrower.Text = "Edit Borrower";
            this.btnEditBorrower.UseVisualStyleBackColor = true;
            //
            // btnDeleteBorrower
            //
            this.btnDeleteBorrower.Location = new System.Drawing.Point(570, 90); // Example position
            this.btnDeleteBorrower.Name = "btnDeleteBorrower";
            this.btnDeleteBorrower.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteBorrower.TabIndex = 3;
            this.btnDeleteBorrower.Text = "Delete Borrower";
            this.btnDeleteBorrower.UseVisualStyleBackColor = true;
            //
            // btnIssueBook
            //
            this.btnIssueBook.Location = new System.Drawing.Point(570, 130); // Example position
            this.btnIssueBook.Name = "btnIssueBook";
            this.btnIssueBook.Size = new System.Drawing.Size(120, 30);
            this.btnIssueBook.TabIndex = 4;
            this.btnIssueBook.Text = "Issue Book";
            this.btnIssueBook.UseVisualStyleBackColor = true;
            //
            // btnReturnBook
            //
            this.btnReturnBook.Location = new System.Drawing.Point(570, 170); // Example position
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(120, 30);
            this.btnReturnBook.TabIndex = 5;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            //
            // dgvIssuedBooks
            //
            this.dgvIssuedBooks.AllowUserToAddRows = false;
            this.dgvIssuedBooks.AllowUserToDeleteRows = false;
            this.dgvIssuedBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIssuedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIssuedBooks.Location = new System.Drawing.Point(10, 220); // Position below dgvBorrowers
            this.dgvIssuedBooks.MultiSelect = false;
            this.dgvIssuedBooks.Name = "dgvIssuedBooks";
            this.dgvIssuedBooks.ReadOnly = true;
            this.dgvIssuedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIssuedBooks.Size = new System.Drawing.Size(770, 340); // Fills remaining space
            this.dgvIssuedBooks.TabIndex = 6;
            //
            // mainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600); // Example client size
            this.Controls.Add(this.tabControlMain);
            this.Name = "mainForm";
            this.Text = "MyLibrary - Main Application";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.tabPageBorrowers.ResumeLayout(false); // Added for Borrowers tab
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).EndInit(); // Added for Borrowers tab
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).EndInit(); // Added for Issued Books display
            this.ResumeLayout(false);

        }

        #endregion

        // These are the controls you interact with in MainForm.cs
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBooks;
        private System.Windows.Forms.TabPage tabPageBorrowers;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnEditBook;
        private System.Windows.Forms.Button btnDeleteBook;

        // Added controls for Borrowers Management tab
        private System.Windows.Forms.DataGridView dgvBorrowers;
        private System.Windows.Forms.Button btnAddBorrower;
        private System.Windows.Forms.Button btnEditBorrower;
        private System.Windows.Forms.Button btnDeleteBorrower;
        private System.Windows.Forms.Button btnIssueBook;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.DataGridView dgvIssuedBooks; // For displaying issued books
    }
}
