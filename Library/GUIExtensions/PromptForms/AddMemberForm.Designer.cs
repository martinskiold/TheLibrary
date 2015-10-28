namespace Library.GUIExtensions.PromptForms
{
    partial class AddMemberForm
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
            this.gbAddMember = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAddMemberId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAddMemberName = new System.Windows.Forms.TextBox();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnCancelAddBook = new System.Windows.Forms.Button();
            this.gbAddMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAddMember
            // 
            this.gbAddMember.Controls.Add(this.label2);
            this.gbAddMember.Controls.Add(this.tbAddMemberId);
            this.gbAddMember.Controls.Add(this.label1);
            this.gbAddMember.Controls.Add(this.tbAddMemberName);
            this.gbAddMember.Controls.Add(this.btnAddMember);
            this.gbAddMember.Controls.Add(this.btnCancelAddBook);
            this.gbAddMember.Location = new System.Drawing.Point(12, 12);
            this.gbAddMember.Name = "gbAddMember";
            this.gbAddMember.Size = new System.Drawing.Size(260, 144);
            this.gbAddMember.TabIndex = 4;
            this.gbAddMember.TabStop = false;
            this.gbAddMember.Text = "Add Member";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Member\'s Personal Id";
            // 
            // tbAddMemberId
            // 
            this.tbAddMemberId.Location = new System.Drawing.Point(123, 62);
            this.tbAddMemberId.Name = "tbAddMemberId";
            this.tbAddMemberId.Size = new System.Drawing.Size(120, 20);
            this.tbAddMemberId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Member\'s Name";
            // 
            // tbAddMemberName
            // 
            this.tbAddMemberName.Location = new System.Drawing.Point(123, 36);
            this.tbAddMemberName.Name = "tbAddMemberName";
            this.tbAddMemberName.Size = new System.Drawing.Size(120, 20);
            this.tbAddMemberName.TabIndex = 0;
            // 
            // btnAddMember
            // 
            this.btnAddMember.Location = new System.Drawing.Point(123, 102);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(75, 23);
            this.btnAddMember.TabIndex = 3;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnCancelAddBook
            // 
            this.btnCancelAddBook.Location = new System.Drawing.Point(42, 102);
            this.btnCancelAddBook.Name = "btnCancelAddBook";
            this.btnCancelAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnCancelAddBook.TabIndex = 2;
            this.btnCancelAddBook.Text = "Cancel";
            this.btnCancelAddBook.UseVisualStyleBackColor = true;
            this.btnCancelAddBook.Click += new System.EventHandler(this.btnCancelAddBook_Click);
            // 
            // AddMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 168);
            this.Controls.Add(this.gbAddMember);
            this.Name = "AddMemberForm";
            this.Text = "AddMemberForm";
            this.Load += new System.EventHandler(this.AddMemberForm_Load);
            this.gbAddMember.ResumeLayout(false);
            this.gbAddMember.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAddMember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAddMemberName;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnCancelAddBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAddMemberId;
    }
}