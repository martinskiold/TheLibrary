namespace Library.PromptForms
{
    partial class AddAuthorForm
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
            this.gbAddAuthor = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAddAuthor = new System.Windows.Forms.TextBox();
            this.btnAddAuthor = new System.Windows.Forms.Button();
            this.btnCancelAddBook = new System.Windows.Forms.Button();
            this.gbAddAuthor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAddAuthor
            // 
            this.gbAddAuthor.Controls.Add(this.label1);
            this.gbAddAuthor.Controls.Add(this.tbAddAuthor);
            this.gbAddAuthor.Controls.Add(this.btnAddAuthor);
            this.gbAddAuthor.Controls.Add(this.btnCancelAddBook);
            this.gbAddAuthor.Location = new System.Drawing.Point(12, 12);
            this.gbAddAuthor.Name = "gbAddAuthor";
            this.gbAddAuthor.Size = new System.Drawing.Size(254, 137);
            this.gbAddAuthor.TabIndex = 3;
            this.gbAddAuthor.TabStop = false;
            this.gbAddAuthor.Text = "Add Author";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Author\'s Name";
            // 
            // tbAddAuthor
            // 
            this.tbAddAuthor.Location = new System.Drawing.Point(104, 36);
            this.tbAddAuthor.Name = "tbAddAuthor";
            this.tbAddAuthor.Size = new System.Drawing.Size(120, 20);
            this.tbAddAuthor.TabIndex = 0;
            // 
            // btnAddAuthor
            // 
            this.btnAddAuthor.Location = new System.Drawing.Point(127, 84);
            this.btnAddAuthor.Name = "btnAddAuthor";
            this.btnAddAuthor.Size = new System.Drawing.Size(75, 23);
            this.btnAddAuthor.TabIndex = 1;
            this.btnAddAuthor.Text = "Add Author";
            this.btnAddAuthor.UseVisualStyleBackColor = true;
            this.btnAddAuthor.Click += new System.EventHandler(this.btnAddAuthor_Click);
            // 
            // btnCancelAddBook
            // 
            this.btnCancelAddBook.Location = new System.Drawing.Point(46, 84);
            this.btnCancelAddBook.Name = "btnCancelAddBook";
            this.btnCancelAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddBook.TabIndex = 0;
            this.btnCancelAddBook.Text = "Cancel";
            this.btnCancelAddBook.UseVisualStyleBackColor = true;
            this.btnCancelAddBook.Click += new System.EventHandler(this.btnCancelAddBook_Click);
            // 
            // AddAuthorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 161);
            this.Controls.Add(this.gbAddAuthor);
            this.Name = "AddAuthorForm";
            this.Text = "AddAuthor";
            this.Load += new System.EventHandler(this.AddAuthorForm_Load);
            this.gbAddAuthor.ResumeLayout(false);
            this.gbAddAuthor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAddAuthor;
        private System.Windows.Forms.Button btnAddAuthor;
        private System.Windows.Forms.Button btnCancelAddBook;
        private System.Windows.Forms.TextBox tbAddAuthor;
        private System.Windows.Forms.Label label1;

    }
}