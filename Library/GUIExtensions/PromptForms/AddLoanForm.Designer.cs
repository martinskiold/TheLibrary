namespace Library.GUIExtensions.PromptForms
{
    partial class AddLoanForm
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
            this.gbAddLoan = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMembers = new System.Windows.Forms.ComboBox();
            this.lvBookCopies = new System.Windows.Forms.ListView();
            this.lvBookCopiesId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBookCopiesTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBooks = new System.Windows.Forms.ListView();
            this.lvBooksBookId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBooksBookISBN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBooksBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBooksBookDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBooksAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnCancelAddBook = new System.Windows.Forms.Button();
            this.gbAddLoan.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAddLoan
            // 
            this.gbAddLoan.Controls.Add(this.groupBox1);
            this.gbAddLoan.Controls.Add(this.lvBookCopies);
            this.gbAddLoan.Controls.Add(this.lvBooks);
            this.gbAddLoan.Controls.Add(this.btnAddBook);
            this.gbAddLoan.Controls.Add(this.btnCancelAddBook);
            this.gbAddLoan.Location = new System.Drawing.Point(12, 12);
            this.gbAddLoan.Name = "gbAddLoan";
            this.gbAddLoan.Size = new System.Drawing.Size(760, 442);
            this.gbAddLoan.TabIndex = 3;
            this.gbAddLoan.TabStop = false;
            this.gbAddLoan.Text = "Select a book and a member to make a loan";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbMembers);
            this.groupBox1.Location = new System.Drawing.Point(257, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 150);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a book and a member to make a loan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select a Member";
            // 
            // cbMembers
            // 
            this.cbMembers.FormattingEnabled = true;
            this.cbMembers.Location = new System.Drawing.Point(74, 64);
            this.cbMembers.Name = "cbMembers";
            this.cbMembers.Size = new System.Drawing.Size(336, 21);
            this.cbMembers.TabIndex = 8;
            // 
            // lvBookCopies
            // 
            this.lvBookCopies.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lvBookCopies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvBookCopiesId,
            this.lvBookCopiesTitle});
            this.lvBookCopies.FullRowSelect = true;
            this.lvBookCopies.Location = new System.Drawing.Point(6, 199);
            this.lvBookCopies.Name = "lvBookCopies";
            this.lvBookCopies.Size = new System.Drawing.Size(245, 143);
            this.lvBookCopies.TabIndex = 4;
            this.lvBookCopies.UseCompatibleStateImageBehavior = false;
            this.lvBookCopies.View = System.Windows.Forms.View.Details;
            this.lvBookCopies.SelectedIndexChanged += new System.EventHandler(this.lvBookCopies_SelectedIndexChanged);
            // 
            // lvBookCopiesId
            // 
            this.lvBookCopiesId.Text = "BookCopy Id";
            this.lvBookCopiesId.Width = 76;
            // 
            // lvBookCopiesTitle
            // 
            this.lvBookCopiesTitle.Text = "Book Title";
            this.lvBookCopiesTitle.Width = 165;
            // 
            // lvBooks
            // 
            this.lvBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvBooksBookId,
            this.lvBooksBookISBN,
            this.lvBooksBookTitle,
            this.lvBooksBookDescription,
            this.lvBooksAuthor});
            this.lvBooks.FullRowSelect = true;
            this.lvBooks.Location = new System.Drawing.Point(6, 19);
            this.lvBooks.Name = "lvBooks";
            this.lvBooks.Size = new System.Drawing.Size(748, 167);
            this.lvBooks.TabIndex = 3;
            this.lvBooks.UseCompatibleStateImageBehavior = false;
            this.lvBooks.View = System.Windows.Forms.View.Details;
            this.lvBooks.SelectedIndexChanged += new System.EventHandler(this.lvBooks_SelectedIndexChanged);
            // 
            // lvBooksBookId
            // 
            this.lvBooksBookId.Text = "Id";
            this.lvBooksBookId.Width = 33;
            // 
            // lvBooksBookISBN
            // 
            this.lvBooksBookISBN.DisplayIndex = 2;
            this.lvBooksBookISBN.Text = "ISBN";
            this.lvBooksBookISBN.Width = 92;
            // 
            // lvBooksBookTitle
            // 
            this.lvBooksBookTitle.DisplayIndex = 1;
            this.lvBooksBookTitle.Text = "Title";
            this.lvBooksBookTitle.Width = 167;
            // 
            // lvBooksBookDescription
            // 
            this.lvBooksBookDescription.Text = "Description";
            this.lvBooksBookDescription.Width = 301;
            // 
            // lvBooksAuthor
            // 
            this.lvBooksAuthor.Text = "Author";
            this.lvBooksAuthor.Width = 151;
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(616, 396);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "Add Loan";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // btnCancelAddBook
            // 
            this.btnCancelAddBook.Location = new System.Drawing.Point(74, 396);
            this.btnCancelAddBook.Name = "btnCancelAddBook";
            this.btnCancelAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddBook.TabIndex = 0;
            this.btnCancelAddBook.Text = "Cancel";
            this.btnCancelAddBook.UseVisualStyleBackColor = true;
            this.btnCancelAddBook.Click += new System.EventHandler(this.btnCancelAddBook_Click);
            // 
            // AddLoanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 466);
            this.Controls.Add(this.gbAddLoan);
            this.Name = "AddLoanForm";
            this.Text = "AddLoanForm";
            this.Load += new System.EventHandler(this.AddLoanForm_Load);
            this.gbAddLoan.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAddLoan;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnCancelAddBook;
        private System.Windows.Forms.ListView lvBooks;
        private System.Windows.Forms.ColumnHeader lvBooksBookId;
        private System.Windows.Forms.ColumnHeader lvBooksBookISBN;
        private System.Windows.Forms.ColumnHeader lvBooksBookTitle;
        private System.Windows.Forms.ColumnHeader lvBooksBookDescription;
        private System.Windows.Forms.ColumnHeader lvBooksAuthor;
        private System.Windows.Forms.ListView lvBookCopies;
        private System.Windows.Forms.ColumnHeader lvBookCopiesId;
        private System.Windows.Forms.ColumnHeader lvBookCopiesTitle;
        private System.Windows.Forms.ComboBox cbMembers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}