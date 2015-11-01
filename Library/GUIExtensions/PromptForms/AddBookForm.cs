//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using Library.GUIExtensions;
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
    public partial class AddBookForm : Form
    {
        public AddBookForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Initializing data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBookForm_Load(object sender, EventArgs e)
        {
            // Update the author-list.
            UpdateAuthorList();

            // Listening to changes of the Library's registered Authors.
            LibraryForm.authorService.Updated += (obj, args) =>
            {
                UpdateAuthorList();
            };
        }

        /// <summary>
        /// GUI Update: Updates the Author list.
        /// </summary>
        private void UpdateAuthorList() 
        {
            var authors = LibraryForm.authorService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardAuthorColumnDataMatrix(authors), lvAuthors);
        }

        /// <summary>
        /// On Click: Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddBook_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }

        /// <summary>
        /// On Click: Add Book Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string isbn = tbAddBookIsbn.Text;
            string title = tbAddBookTitle.Text;
            string description = tbAddBookDescription.Text;

            // Input validation.
            if (isbn != "" && title != "" && description != "")
            {
                // Gets the selected author's id.
                int authorId = GUIFunctions.GetSelectedItemIdFromListView(lvAuthors);

                // If there is an author selected.
                if (authorId != -1) 
                {
                    // If the Book was successfully added.
                    if (LibraryForm.bookService.AddBook(isbn, title, description, authorId))
                    {
                        // Close the form.
                        this.Close();
                    }
                    else
                    {
                        // Error Feedback.
                        MessageBox.Show("AddBookError: TheLibraryApplication has encountered an error while trying to add the new Book. The Book has not been registered. Please try again, or call support.", "AddBookError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                }
                else
                {
                    // Prompt for correct input.
                    MessageBox.Show("Please select an author that has written the book or Add a new Author using the Add Author button.");
                }
            }
            else
            {
                // Prompt for input.
                MessageBox.Show("Please input a ISBN-number, a Title, and a Description for the book.");
            }
        }

        /// <summary>
        /// On Click: Add Author button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            // Open the form for adding an Author.
            AddAuthorForm form = new AddAuthorForm();
            form.Show();
        }
    }
}
