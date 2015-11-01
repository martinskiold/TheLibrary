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

        public LibraryForm() {
            InitializeComponent();
            
            // Initialize the repositoryfactory.
            RepositoryFactory repoFactory = new RepositoryFactory();

            // Initialize services by connecting services to the repository factory.
            bookService = new BookService(repoFactory);
            authorService = new AuthorService(repoFactory);
            bookCopyService = new BookCopyService(repoFactory);
            memberService = new MemberService(repoFactory);
            loanService = new LoanService(repoFactory);
        }

        /// <summary>
        /// On GUI Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LibraryForm_Load(object sender, EventArgs e)
        {
            // Initialization of event-subscribers.
            InitializeEventSubscribers();
            // Print data from the database.
            InitializingPrint();
        }
        
        /* Initializers */

        /// <summary>
        /// Initializes subscriptions to different events.
        /// </summary>
        private void InitializeEventSubscribers() 
        {
            //Event Subscriptions.
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
                    UpdateBookLists();
                    UpdateBookCopyLists(-1);
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
        

        /* ---- GUI Updates ---- */

        /// <summary>
        /// GUI Update: Updates all lists that shows Book's @ The Library.
        /// </summary>
        private void UpdateBookLists()
        {
            // All booklists.
            var books = bookService.All();
            // Creates a datamatrix of the books-collection.
            var dataMatrix = ColumnDataMapper.StandardBookColumnDataMatrix(books);
            // Prints the datamatrix to the listview lvBooks.
            ListViewPrinter.UpdateAllRows(dataMatrix, lvBooks);

            // Update available books as well.
            UpdateAvailableBookLists();
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows available Book's @ The Library.
        /// </summary>
        private void UpdateAvailableBookLists() 
        {
            var availableBooks = bookService.BooksWithAvailableCopies();
            var dataMatrix = ColumnDataMapper.StandardBookColumnDataMatrix(availableBooks);
            ListViewPrinter.UpdateAllRows(dataMatrix, lvIntroBooks);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows search result of Book's @ The Library.
        /// </summary>
        private void UpdateSearchBookResults(string keyword) 
        {
            var searchResult = bookService.AvailableBooksSearch(keyword);
            var searchResultMatrix = ColumnDataMapper.StandardBookColumnDataMatrix(searchResult);
            ListViewPrinter.UpdateAllRows(searchResultMatrix, lvIntroBooks);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows BookCopies of a Book @ The Library.
        /// </summary>
        private void UpdateBookCopyLists(int bookId)
        {
            // There is no specified book.
            if (bookId != -1)
            {
                // Clear the BookCopy list.
                lvBookCopies.Items.Clear();
            }

            // BookCopylists connected to a book.
            var copiesOfBook = bookService.AvailableCopies(bookId);
            var dataMatrix = ColumnDataMapper.StandardBookCopyColumnDataMatrix(copiesOfBook);
            ListViewPrinter.UpdateAllRows(dataMatrix, lvBookCopies);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows available BookCopies @ The Library.
        /// </summary>
        private void UpdateAvailableBookCopyLists()
        {
            // Available & not available bookcopylists.
            var availableBookCopies = bookCopyService.AvailableBookCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.BookCopyIncludingBookColumnDataMatrix(availableBookCopies), lvBookCopiesAvailable);
            var notAvailableBookCopies = bookCopyService.NotAvailableBookCopies();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.BookCopyIncludingBookColumnDataMatrix(notAvailableBookCopies), lvBookCopiesNotAvailable);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows Authors @ The Library.
        /// </summary>
        private void UpdateAuthorLists()
        {
            var authors = authorService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardAuthorColumnDataMatrix(authors), lvAuthors);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows the Author's books @ The Library.
        /// </summary>
        private void UpdateAuthorsBooksList(int authorId) 
        {
            // Books written by the author.
            IEnumerable<Book> books = authorService.BooksFromAuthor(authorId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.CompactBookColumnDataMatrix(books), lvAuthorsBooks);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows registered Members @ The Library.
        /// </summary>
        private void UpdateMemberLists()
        {
            var members = memberService.All();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardMemberColumnDataMatrix(members), lvMembers);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows the Member's Loans @ The Library.
        /// </summary>
        private void UpdateMembersLoansLists(int memberId) 
        {
            // The members currently active loans.
            var loans = memberService.CurrentLoans(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loans), lvMembersLoans);

            // The members currently inactive loans.
            var loansReturned = memberService.ReturnedLoans(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loansReturned), lvMembersLoansReturned);

            // The members currently active and overdue loans.
            var loansOverdue = memberService.LoansCurrentlyOverdue(memberId);
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(loansOverdue), lvMembersLoansOverdue);
        }

        /// <summary>
        /// GUI Update: Updates all lists that shows Loans @ The Library.
        /// </summary>
        private void UpdateLoanLists()
        {
            // All active loans.
            var currentLoans = loanService.AllCurrent();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(currentLoans), lvLoansCurrent);
            // All active and overdue loans.
            var currentlyOverdue = loanService.AllCurrentlyOverdue();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(currentlyOverdue), lvLoansOverdue);
            // All inactive and returned loans.
            var returnedLoans = loanService.AllReturned();
            ListViewPrinter.UpdateAllRows(ColumnDataMapper.StandardLoanColumnDataMatrix(returnedLoans), lvLoansReturned);
        }


        /* ---- GUI Events ---- */

        /* TABPAGE: 'Home' */

        /// <summary>
        /// On Click: Search button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string input = tbSearchBooks.Text;
            UpdateSearchBookResults(input);
        }

        /* TABPAGE: 'Books & Authors' */

        /// <summary>
        /// On Click: Add Book button on "Books & Authors"-page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            // Display form for adding a new book.
            AddBookForm form = new AddBookForm();
            form.Show();
        }

        /// <summary>
        /// On Click: Add Author button on "Books & Authors"-page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            // Display form for adding a new author.
            AddAuthorForm form = new AddAuthorForm();
            form.Show();
        }

        /// <summary>
        /// On Click: Add BookCopy button on "Books & Authors"-page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBookCopy_Click(object sender, EventArgs e)
        {
            // Get selected book.
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);

            // Selection validation.
            if(bookId != -1)
            {
                // If bookcopy was successfully added.
                if (bookCopyService.AddBookCopy(bookId))
                {
                    // Select the ListView control.
                    lvBooks.Select();
                }
                else
                {
                    // Error Feedback.
                    MessageBox.Show("AddCopyError: TheLibraryApplication has encountered an error while trying to register the new BookCopy. The BookCopy has not been registered. Please try again, or call support.", "AddCopyError:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Prompt for user input.
                MessageBox.Show("Please select a book to add an extra copy of");
            }
        }

        /// <summary>
        /// Selection Event: ListView of Books.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bookId = GUIFunctions.GetSelectedItemIdFromListView(lvBooks);
            UpdateBookCopyLists(bookId);
        }

        /// <summary>
        /// Selection Event: ListView of Authors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int authorId = GUIFunctions.GetSelectedItemIdFromListView(lvAuthors);
            UpdateAuthorsBooksList(authorId);
        }




        /* TABPAGE: 'Members & Loans' */

        /// <summary>
        /// On Click: Add Member button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            // Display form to add a member.
            AddMemberForm form = new AddMemberForm();
            form.Show();
        }

        /// <summary>
        /// On Click: Add Loan button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            // Display form to add a loan.
            AddLoanForm form = new AddLoanForm();
            form.Show();
        }

        /// <summary>
        /// Selection Event: ListView of members.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int memberId = GUIFunctions.GetSelectedItemIdFromListView(lvMembers);
            UpdateMembersLoansLists(memberId);
        }

        /// <summary>
        /// On Click: Add Fake Overdue button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOverdue_Click(object sender, EventArgs e)
        {
            int loanId = GUIFunctions.GetSelectedItemIdFromListView(lvMembersLoans);
            // If there is a selected loan.
            if (loanId != -1)
            {
                // Fake overdue to selected loan.
                loanService.AddMonthsOverdueToDueDate(loanId, -1);
            }
            else
            {
                // Prompt for user input.
                MessageBox.Show("Please select a loan from a members current loans before doing this operation.");
            }
        }

        /// <summary>
        /// On Click: Return Book button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            int loanId = GUIFunctions.GetSelectedItemIdFromListView(lvMembersLoans);

            // Check if there is a selected loan.
            if (loanId != -1)
            {
                // Retrieves the fine if the loan has passed its duedate.
                int fine = loanService.ReturnBook(loanId);
                if (fine != 0)
                {
                    // Shows a message that the book was returned overdue. It shows the size of the Fine.
                    MessageBox.Show(string.Format("This loan has passed its Duedate. You will therefor be fined [{0}kr] for passing the DueDate with your book return.", fine), "This Loan has passed it's duedate!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // Shows a message that the book was returned on time.
                    MessageBox.Show("Your book was successfully returned! Please come again!");
                }
            }
            else
            {
                // Prompt for user input.
                MessageBox.Show("Please select a Loan from a Members Current Loans before doing this operation.");
            }
        }

    }

}
