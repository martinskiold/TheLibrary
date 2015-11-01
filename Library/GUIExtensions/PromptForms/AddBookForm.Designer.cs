namespace Library.PromptForms
{
    partial class AddBookForm
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
            this.btnCancelAddBook = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.gbAddBook = new System.Windows.Forms.GroupBox();
            this.gbAddBookInfo = new System.Windows.Forms.GroupBox();
            this.tbAddBookTitle = new System.Windows.Forms.TextBox();
            this.tbAddBookIsbn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAddBookDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbAuthors = new System.Windows.Forms.GroupBox();
            this.btnAddAuthor = new System.Windows.Forms.Button();
            this.lvAuthors = new System.Windows.Forms.ListView();
            this.lvAuthorsId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAuthorsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbAddBook.SuspendLayout();
            this.gbAddBookInfo.SuspendLayout();
            this.gbAuthors.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelAddBook
            // 
            this.btnCancelAddBook.Location = new System.Drawing.Point(101, 313);
            this.btnCancelAddBook.Name = "btnCancelAddBook";
            this.btnCancelAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddBook.TabIndex = 0;
            this.btnCancelAddBook.Text = "Cancel";
            this.btnCancelAddBook.UseVisualStyleBackColor = true;
            this.btnCancelAddBook.Click += new System.EventHandler(this.btnCancelAddBook_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(385, 313);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "Add Book";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // gbAddBook
            // 
            this.gbAddBook.Controls.Add(this.gbAddBookInfo);
            this.gbAddBook.Controls.Add(this.gbAuthors);
            this.gbAddBook.Controls.Add(this.btnAddBook);
            this.gbAddBook.Controls.Add(this.btnCancelAddBook);
            this.gbAddBook.Location = new System.Drawing.Point(12, 12);
            this.gbAddBook.Name = "gbAddBook";
            this.gbAddBook.Size = new System.Drawing.Size(560, 354);
            this.gbAddBook.TabIndex = 2;
            this.gbAddBook.TabStop = false;
            this.gbAddBook.Text = "Add Book";
            // 
            // gbAddBookInfo
            // 
            this.gbAddBookInfo.Controls.Add(this.tbAddBookTitle);
            this.gbAddBookInfo.Controls.Add(this.tbAddBookIsbn);
            this.gbAddBookInfo.Controls.Add(this.label3);
            this.gbAddBookInfo.Controls.Add(this.tbAddBookDescription);
            this.gbAddBookInfo.Controls.Add(this.label2);
            this.gbAddBookInfo.Controls.Add(this.label1);
            this.gbAddBookInfo.Location = new System.Drawing.Point(6, 27);
            this.gbAddBookInfo.Name = "gbAddBookInfo";
            this.gbAddBookInfo.Size = new System.Drawing.Size(267, 261);
            this.gbAddBookInfo.TabIndex = 10;
            this.gbAddBookInfo.TabStop = false;
            this.gbAddBookInfo.Text = "Information about the Book";
            // 
            // tbAddBookTitle
            // 
            this.tbAddBookTitle.Location = new System.Drawing.Point(84, 56);
            this.tbAddBookTitle.Name = "tbAddBookTitle";
            this.tbAddBookTitle.Size = new System.Drawing.Size(100, 20);
            this.tbAddBookTitle.TabIndex = 3;
            // 
            // tbAddBookIsbn
            // 
            this.tbAddBookIsbn.Location = new System.Drawing.Point(84, 30);
            this.tbAddBookIsbn.Name = "tbAddBookIsbn";
            this.tbAddBookIsbn.Size = new System.Drawing.Size(100, 20);
            this.tbAddBookIsbn.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description";
            // 
            // tbAddBookDescription
            // 
            this.tbAddBookDescription.Location = new System.Drawing.Point(21, 128);
            this.tbAddBookDescription.Multiline = true;
            this.tbAddBookDescription.Name = "tbAddBookDescription";
            this.tbAddBookDescription.Size = new System.Drawing.Size(227, 113);
            this.tbAddBookDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Title";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ISBN";
            // 
            // gbAuthors
            // 
            this.gbAuthors.Controls.Add(this.btnAddAuthor);
            this.gbAuthors.Controls.Add(this.lvAuthors);
            this.gbAuthors.Location = new System.Drawing.Point(279, 27);
            this.gbAuthors.Name = "gbAuthors";
            this.gbAuthors.Size = new System.Drawing.Size(275, 261);
            this.gbAuthors.TabIndex = 9;
            this.gbAuthors.TabStop = false;
            this.gbAuthors.Text = "Select an Author to the Book";
            // 
            // btnAddAuthor
            // 
            this.btnAddAuthor.Location = new System.Drawing.Point(82, 218);
            this.btnAddAuthor.Name = "btnAddAuthor";
            this.btnAddAuthor.Size = new System.Drawing.Size(122, 23);
            this.btnAddAuthor.TabIndex = 11;
            this.btnAddAuthor.Text = "Add Author";
            this.btnAddAuthor.UseVisualStyleBackColor = true;
            this.btnAddAuthor.Click += new System.EventHandler(this.btnAddAuthor_Click);
            // 
            // lvAuthors
            // 
            this.lvAuthors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvAuthorsId,
            this.lvAuthorsName});
            this.lvAuthors.FullRowSelect = true;
            this.lvAuthors.HideSelection = false;
            this.lvAuthors.Location = new System.Drawing.Point(44, 30);
            this.lvAuthors.MultiSelect = false;
            this.lvAuthors.Name = "lvAuthors";
            this.lvAuthors.Size = new System.Drawing.Size(189, 173);
            this.lvAuthors.TabIndex = 8;
            this.lvAuthors.UseCompatibleStateImageBehavior = false;
            this.lvAuthors.View = System.Windows.Forms.View.Details;
            // 
            // lvAuthorsId
            // 
            this.lvAuthorsId.Text = "Author Id";
            this.lvAuthorsId.Width = 55;
            // 
            // lvAuthorsName
            // 
            this.lvAuthorsName.Text = "Name";
            this.lvAuthorsName.Width = 129;
            // 
            // AddBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 378);
            this.Controls.Add(this.gbAddBook);
            this.Name = "AddBookForm";
            this.Text = "Add Book";
            this.Load += new System.EventHandler(this.AddBookForm_Load);
            this.gbAddBook.ResumeLayout(false);
            this.gbAddBookInfo.ResumeLayout(false);
            this.gbAddBookInfo.PerformLayout();
            this.gbAuthors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelAddBook;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.GroupBox gbAddBook;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAddBookDescription;
        private System.Windows.Forms.TextBox tbAddBookTitle;
        private System.Windows.Forms.TextBox tbAddBookIsbn;
        private System.Windows.Forms.GroupBox gbAddBookInfo;
        private System.Windows.Forms.GroupBox gbAuthors;
        private System.Windows.Forms.ListView lvAuthors;
        private System.Windows.Forms.ColumnHeader lvAuthorsId;
        private System.Windows.Forms.ColumnHeader lvAuthorsName;
        private System.Windows.Forms.Button btnAddAuthor;
    }
}