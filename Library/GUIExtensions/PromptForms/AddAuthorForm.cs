//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.PromptForms
{
    public partial class AddAuthorForm : Form
    {
        public AddAuthorForm()
        {
            InitializeComponent();
        }

        private void gbAuthors_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// On Click: Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddBook_Click(object sender, EventArgs e)
        {
            // Close form.
            this.Close();
        }

        /// <summary>
        /// On Click: Add Author button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            string authorName = tbAddAuthor.Text;

            // Validate input
            if (authorName != "")
            {
                // If adding the new author succeeds.
                if (LibraryForm.authorService.AddAuthor(authorName))
                {
                    // Close form.
                    this.Close();
                }
                else
                {
                    // Error Feedback.
                    MessageBox.Show("AddAuthorError: TheLibraryApplication has encountered an error while trying to register the new Author. The Author has not been registered. Please try again, or call support.","AddAuthorError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Prompt for input.
                MessageBox.Show("Please input the Author's name.");
            }
        }

        private void AddAuthorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
