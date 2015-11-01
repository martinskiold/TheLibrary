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

namespace Library.GUIExtensions.PromptForms
{
    public partial class AddLoanForm : Form
    {
        public AddLoanForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// GUI Update: Updates the book lists.
        /// </summary>
        private void UpdateBookList()
        {
            var availableBooks = LibraryForm.bookService.BooksWithAvailableCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookColumnDataMatrix(availableBooks), lvBooks);
        }
        /// <summary>
        /// GUI Update: Updates the member-selection dropdown menu.
        /// </summary>
        private void UpdateMemberDropdown() 
        {
            var members = LibraryForm.memberService.All();
            foreach (Member m in members)
            {
                // Add Member to the dropdown menu.
                cbMembers.Items.Add(m);
            }
        }

        /// <summary>
        /// GUI Update: Updates lists with BookCopies.
        /// </summary>
        /// <param name="bookId"></param>
        private void UpdateBookCopyList(int bookId) 
        {
            var bookCopies = LibraryForm.bookService.AvailableCopies(bookId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookCopyColumnDataMatrix(bookCopies), lvBookCopies);
        }

        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLoanForm_Load(object sender, EventArgs e)
        {
            UpdateBookList();
            UpdateMemberDropdown();
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
        /// Selection event when choosing a book.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            if (bookId != -1) 
            {
                // Update the bookcopies related to the book.
                UpdateBookCopyList(bookId);
            }
        }

        /// <summary>
        /// On Click: Add book button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            // Gets the selected book.
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            // Gets the selected member from the dropdown-menu.
            Member member = (Member) cbMembers.SelectedItem;
            // Input validation.
            if (bookId != -1 && member != null)
            {
                // If adding the loan succeeds.
                if (LibraryForm.loanService.AddLoan(bookId, member.Id))
                {
                    // Close the form.
                    this.Close();
                }
                else
                {
                    // Error Feedback.
                    MessageBox.Show("AddLoanError: TheLibraryApplication has encountered an error while registering the loan. The loan has not been registered. Please try again, or call support.", "AddLoanError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                // Prompt for user-input.
                MessageBox.Show("Please select a book and a member to register a loan.");
            }
        }


        private void lvBookCopies_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

    }
}
