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

        private void UpdateBookList()
        {
            var availableBooks = LibraryForm.bookService.BooksWithAvailableCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookColumnDataMatrix(availableBooks), lvBooks);
        }
        private void UpdateMemberDropdown() 
        {
            var members = LibraryForm.memberService.All();
            foreach (Member m in members)
            {
                cbMembers.Items.Add(m);
            }
        }
        private void UpdateBookCopyList(int bookId) 
        {
            var bookCopies = LibraryForm.bookService.AvailableCopies(bookId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookCopyColumnDataMatrix(bookCopies), lvBookCopies);
        }

        private void AddLoanForm_Load(object sender, EventArgs e)
        {
            UpdateBookList();
            UpdateMemberDropdown();
        }

        private void btnCancelAddBook_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            if (bookId != -1) 
            {
                UpdateBookCopyList(bookId);
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            Member member = (Member) cbMembers.SelectedItem;
            if (bookId != -1 && member != null)
            {
                if (LibraryForm.loanService.AddLoan(bookId, member.Id))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("AddLoanError: TheLibraryApplication has encountered an error while registering the loan. The loan has not been registered. Please try again, or call support.", "AddLoanError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please select a book and a member to register a loan.");
            }
        }


        private void lvBookCopies_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

    }
}
