namespace Mylibrary3
{
    partial class BookForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.lblTotalCopies = new System.Windows.Forms.Label();
            this.numTotalCopies = new System.Windows.Forms.NumericUpDown();
            this.lblAvailableCopies = new System.Windows.Forms.Label();
            this.numAvailableCopies = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalCopies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAvailableCopies)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 30); // Example position
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            //
            // txtTitle
            //
            this.txtTitle.Location = new System.Drawing.Point(130, 27); // Example position
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 20); // Example size
            this.txtTitle.TabIndex = 1;
            //
            // lblAuthor
            //
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(30, 60); // Example position
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author:";
            //
            // txtAuthor
            //
            this.txtAuthor.Location = new System.Drawing.Point(130, 57); // Example position
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(200, 20);
            this.txtAuthor.TabIndex = 3;
            //
            // lblYear
            //
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(30, 90); // Example position
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 13);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Year:";
            //
            // numYear
            //
            this.numYear.Location = new System.Drawing.Point(130, 87); // Example position
            this.numYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(80, 20); // Example size
            this.numYear.TabIndex = 5;
            this.numYear.Value = new decimal(new int[] {
            2024, // Default to current year or similar
            0,
            0,
            0});
            //
            // lblTotalCopies
            //
            this.lblTotalCopies.AutoSize = true;
            this.lblTotalCopies.Location = new System.Drawing.Point(30, 120); // Example position
            this.lblTotalCopies.Name = "lblTotalCopies";
            this.lblTotalCopies.Size = new System.Drawing.Size(69, 13);
            this.lblTotalCopies.TabIndex = 6;
            this.lblTotalCopies.Text = "Total Copies:";
            //
            // numTotalCopies
            //
            this.numTotalCopies.Location = new System.Drawing.Point(130, 117); // Example position
            this.numTotalCopies.Maximum = new decimal(new int[] {
            1000, // Example max
            0,
            0,
            0});
            this.numTotalCopies.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTotalCopies.Name = "numTotalCopies";
            this.numTotalCopies.Size = new System.Drawing.Size(80, 20);
            this.numTotalCopies.TabIndex = 7;
            this.numTotalCopies.Value = new decimal(new int[] {
            1, // Default to 1
            0,
            0,
            0});
            //
            // lblAvailableCopies
            //
            this.lblAvailableCopies.AutoSize = true;
            this.lblAvailableCopies.Location = new System.Drawing.Point(30, 150);
            this.lblAvailableCopies.Name = "lblAvailableCopies";
            this.lblAvailableCopies.Size = new System.Drawing.Size(90, 13);
            this.lblAvailableCopies.TabIndex = 8;
            this.lblAvailableCopies.Text = "Available Copies:";
            //
            // numAvailableCopies
            //
            this.numAvailableCopies.Location = new System.Drawing.Point(130, 147);
            this.numAvailableCopies.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAvailableCopies.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAvailableCopies.Name = "numAvailableCopies";
            this.numAvailableCopies.Size = new System.Drawing.Size(80, 20);
            this.numAvailableCopies.TabIndex = 9;
            this.numAvailableCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // btnSave
            //
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(130, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(215, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // BookForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numAvailableCopies);
            this.Controls.Add(this.lblAvailableCopies);
            this.Controls.Add(this.numTotalCopies);
            this.Controls.Add(this.lblTotalCopies);
            this.Controls.Add(this.numYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BookForm";
            // Removed: this.Load += new System.EventHandler(this.BookForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalCopies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAvailableCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // These are the declarations of the UI controls, accessible in BookForm.cs
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Label lblTotalCopies;
        private System.Windows.Forms.NumericUpDown numTotalCopies;
        private System.Windows.Forms.Label lblAvailableCopies;
        private System.Windows.Forms.NumericUpDown numAvailableCopies;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

