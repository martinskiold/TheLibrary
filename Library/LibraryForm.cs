//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using Library.PromptForms;
using Library.GUIExtensions.PromptForms;
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

namespace Library {

    public partial class LibraryForm : Form {

        // Instance fields for services.
        public static BookService bookService;
        public static AuthorService authorService;
        public static BookCopyService bookCopyService;
        public static MemberService memberService;
        public static LoanService loanService;

        // Instance fields for printers. (Outputs to GUI)
        /*public static ListViewPrinter<Book> lvpBooks;
        public static ListViewPrinter<BookCopy> lvpBookCopies;
        public static ListViewPrinter<Author> lvpAuthors;
        public static ListViewPrinter<Member> lvpMembers;
        public static ListViewPrinter<Loan> lvpLoans;*/
        

        public LibraryForm() {
            InitializeComponent();
            
            // Initialize the repositoryfactory.
            RepositoryFactory repoFactory = new RepositoryFactory();

            // Initialize services by connecting services to their corresponding repositories.
            bookService = new BookService(repoFactory);
            authorService = new AuthorService(repoFactory);
            bookCopyService = new BookCopyService(repoFactory);
            memberService = new MemberService(repoFactory);
            loanService = new LoanService(repoFactory);
            int test = 0;
        }

        /// <summary>
        /// On GUI Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LibraryForm_Load(object sender, EventArgs e)
        {
            InitializeEventSubscribers();
            InitializingPrint();
        }
        
        /* Initializers */

        /// <summary>
        /// Initializes subscriptions to different events.
        /// </summary>
        private void InitializeEventSubscribers() 
        {
            //EventhandlerSubscriptions
            bookService.Updated += (obj, args) =>
                {
                    UpdateBookLists();
                    UpdateAvailableBookCopyLists();
                    UpdateAvailableBookLists();
                };
            bookCopyService.Updated += (obj, args) =>
                {
                    UpdateBookCopyLists(args.Item.Book.Id);
                    UpdateAvailableBookLists();
                    UpdateAvailableBookCopyLists();
                };
            authorService.Updated += (obj, args) =>
                {
                    UpdateAuthorLists();
                };
            memberService.Updated += (obj, args) =>
                {
                    UpdateMemberLists();
                };
            loanService.Updated += (obj, args) =>
                {
                    UpdateMembersLoansLists(args.Item.Member.Id);
                    UpdateLoanLists();
                    UpdateAvailableBookLists();
                    UpdateAvailableBookCopyLists();
                };
        }

        /// <summary>
        /// Prints database data to all lists in the application.
        /// </summary>
        private void InitializingPrint() 
        {
            // Printing data to GUI.
            UpdateBookLists();
            UpdateAuthorLists();
            UpdateMemberLists();
            UpdateLoanLists();
            UpdateAvailableBookCopyLists();
        }
        

        /* ---- Updates ---- */


        private void UpdateBookLists()
        {
            // All booklists.

            var books = bookService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookColumnDataMatrix(books), lvBooks);
            UpdateAvailableBookLists();
        }
        private void UpdateAvailableBookLists() 
        {
            // Available booklists.

            var availableBooks = bookService.BooksWithAvailableCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookColumnDataMatrix(availableBooks), lvIntroBooks);
        }
        private void UpdateSearchBookResults(string keyword) 
        {
            // Searched Available booklists.

            var searchResult = bookService.AvailableBooksSearch(keyword);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookColumnDataMatrix(searchResult), lvIntroBooks);
        }
        private void UpdateBookCopyLists(int bookId)
        {
            // BookCopylists connected to a book.

            var copiesOfBook = bookService.AllCopiesOfBook(bookId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardBookCopyColumnDataMatrix(copiesOfBook), lvBookCopies);
        }
        private void UpdateAvailableBookCopyLists()
        {
            // Available & not available bookcopylists.

            var availableBookCopies = bookCopyService.AvailableBookCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.BookCopyIncludingBookColumnDataMatrix(availableBookCopies), lvBookCopiesAvailable);
            var notAvailableBookCopies = bookCopyService.NotAvailableBookCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.BookCopyIncludingBookColumnDataMatrix(notAvailableBookCopies), lvBookCopiesNotAvailable);
        }
        private void UpdateAuthorLists()
        {
            // Author lists.

            var authors = authorService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardAuthorColumnDataMatrix(authors), lvAuthors);
        }
        private void UpdateAuthorsBooksList(int authorId) 
        {
            // Authors booklist.

            IEnumerable<Book> books = authorService.BooksFromAuthor(authorId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.CompactBookColumnDataMatrix(books), lvAuthorsBooks);
        }

        private void UpdateMemberLists()
        {
            // Memberlists.

            var members = memberService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardMemberColumnDataMatrix(members), lvMembers);
        }
        private void UpdateMembersLoansLists(int memberId) 
        {
            // Loanlists connected to certain member.

            var loans = memberService.CurrentLoans(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loans), lvMembersLoans);

            var loansReturned = memberService.ReturnedLoans(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loansReturned), lvMembersLoansReturned);

            var loansOverdue = memberService.LoansCurrentlyOverdue(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loansOverdue), lvMembersLoansOverdue);
        }
        private void UpdateLoanLists()
        {
            // All loans.

            var currentLoans = loanService.AllCurrent();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(currentLoans), lvLoansCurrent);
            var currentlyOverdue = loanService.AllCurrentlyOverdue();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(currentlyOverdue), lvLoansOverdue);
            var returnedLoans = loanService.AllReturned();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(returnedLoans), lvLoansReturned);
        }


        /* ---- Events ---- */

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm();
            form.Show();
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            AddAuthorForm form = new AddAuthorForm();
            form.Show();
        }

        private void btnAddBookCopy_Click(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            if(bookId != -1)
            {
                if (bookCopyService.AddBookCopy(bookId))
                {
                    lvBooks.Select();
                }
                else
                {
                    MessageBox.Show("AddCopyError: TheLibraryApplication has encountered an error while trying to register the new BookCopy. The BookCopy has not been registered. Please try again, or call support.", "AddCopyError:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to add an extra copy of");
            }
        }

        private void lvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            UpdateBookCopyLists(bookId);
        }

        private void lvAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int authorId = GUIFunctions.GetSelectedItemIdFromListView(lvAuthors);
            UpdateAuthorsBooksList(authorId);
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddMemberForm form = new AddMemberForm();
            form.Show();
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            AddLoanForm form = new AddLoanForm();
            form.Show();
        }

        private void lvMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int memberId = GUIFunctions.GetSelectedItemIdFromListView(lvMembers);
            UpdateMembersLoansLists(memberId);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnAddOverdue_Click(object sender, EventArgs e)
        {
            int loanId = GUIFunctions.GetSelectedItemIdFromListView(lvMembersLoans);
            if (loanId != -1)
            {
                loanService.ChangeDaysToDueDate(loanId, -16);
            }
            else
            {
                MessageBox.Show("Please select a loan from a members current loans before doing this operation.");
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            int loanId = GUIFunctions.GetSelectedItemIdFromListView(lvMembersLoans);
            if (loanId != -1)
            {
                // If the loan has passed its duedate.
                int fine = loanService.ReturnBook(loanId);
                if (fine != 0)
                {
                    MessageBox.Show(string.Format("This loan has passed its Duedate. You will therefor be fined [{0}kr] for passing the DueDate with your book return.", fine), "This Loan has passed it's duedate!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Your book was successfully returned! Please come again!");
                }
            }
            else
            {
                MessageBox.Show("Please select a Loan from a Members Current Loans before doing this operation.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string input = tbSearchBooks.Text;
            UpdateSearchBookResults(input);
        }

    }

}
