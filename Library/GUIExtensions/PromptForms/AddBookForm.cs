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

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            UpdateAuthorList();

            LibraryForm.authorService.Updated += (obj, args) =>
            {
                UpdateAuthorList();
            };
        }

        private void UpdateAuthorList() 
        {
            var authors = LibraryForm.authorService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardAuthorColumnDataMatrix(authors), lvAuthors);
        }

        private void btnCancelAddBook_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string isbn = tbAddBookIsbn.Text;
            string title = tbAddBookTitle.Text;
            string description = tbAddBookDescription.Text;

            if (isbn != "" && title != "" && description != "")
            {
                int authorId = GUIFunctions.GetSelectedItemIdFromListView(lvAuthors);

                if (authorId != -1) 
                {
                    if (LibraryForm.bookService.AddBook(isbn, title, description, authorId))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("AddBookError: TheLibraryApplication has encountered an error while trying to add the new Book. The Book has not been registered. Please try again, or call support.", "AddBookError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                }
                else
                {
                    MessageBox.Show("Please select an author that has written the book or Add a new Author using the Add Author button.");
                }
            }
            else
            {
                // Prompt for input.
                MessageBox.Show("Please input a ISBN-number, a Title, and a Description for the book.");
            }
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            AddAuthorForm form = new AddAuthorForm();
            form.Show();
        }
    }
}
