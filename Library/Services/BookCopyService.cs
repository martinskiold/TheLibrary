//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Repositories;

namespace Library.Services
{
    /// <summary>
    /// Application logic regarding BookCopies.
    /// </summary>
    public class BookCopyService : IService<UpdatedEventArgs<BookCopy>>
    {
        // Handles events triggered from changes to the library's Bookcopies.
        public event EventHandler<UpdatedEventArgs<BookCopy>> Updated;

        // The repositories used by this libraryservice.
        private BookCopyRepository _bookCopyRepository;
        private BookRepository _bookRepository;
        private LoanRepository _loanRepository;

        /// <summary>
        /// Creates the application logic context necessary for handling the BookCopies of The library.
        /// </summary>
        /// <param name="repoFactory"></param>
        public BookCopyService(RepositoryFactory repoFactory) 
        {
            _bookCopyRepository = repoFactory.GetBookCopyRepository();
            _loanRepository = repoFactory.GetLoanRepository();
            _bookRepository = repoFactory.GetBookRepository();
        }

        /// <summary>
        /// Adds a BookCopy to the library.
        /// </summary>
        /// <param name="item"></param>
        private bool Add(BookCopy item)
        {
            try
            {
                _bookCopyRepository.Add(item);
            }
            catch 
            {
                return false;
            }
            UpdatedEventArgs<BookCopy> args = new UpdatedEventArgs<BookCopy>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Adds a BookCopy to the library.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool AddBookCopy(int bookId) 
        {
            Book b = _bookRepository.Find(bookId);
            if (b != null) 
            {
                BookCopy bc = new BookCopy() 
                {
                    Book = b
                };
                return Add(bc);
            }
            return false;
        }

        /// <summary>
        /// Removes a BookCopy from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(BookCopy item)
        {
            try
            {
                _bookCopyRepository.Remove(item);
                UpdatedEventArgs<BookCopy> args = new UpdatedEventArgs<BookCopy>(item);
                OnUpdated(args);
                return true;
            }
            catch 
            {
                return false;
            }

        }

        /// <summary>
        /// Retrieves a BookCopy from the library.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookCopy Find(int id)
        {
            return _bookCopyRepository.Find(id);
        }

        /// <summary>
        /// Edits a BookCopy from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Edit(BookCopy item)
        {
            try
            {
                _bookCopyRepository.Edit(item);
                UpdatedEventArgs<BookCopy> args = new UpdatedEventArgs<BookCopy>(item);
                OnUpdated(args);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all BookCopies from the library.
        /// If there are no BookCopies in the library, it returns a default empty list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> All()
        {
           var bookCopies = _bookCopyRepository.All();
           return bookCopies;
        }

        /// <summary>
        /// Retrieves all available BookCopies.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> AvailableBookCopies()
        {
            // Gets all bookcopies on loan by checking which loans has a bookcopy without a definite return date.
            // Projects filtering of loans into the loans bookcopy.
            var bookCopiesOnLoan = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null).
                Select(l => l.BookCopy);
            // Gets all bookcopies.
            var bookCopies = _bookCopyRepository.All();
            // Gets the difference between both collections. Bookcopies that exist in both 
            // collections are removed from the returned collection.
            // (All Bookcopies - BookCopies On Loan == Available BookCopies).
            var bookCopiesNotOnLoan = Enumerable.Except(bookCopies, bookCopiesOnLoan);
            return bookCopiesNotOnLoan;
        }

        /// <summary>
        /// Retrieves all BookCopies on loan.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> NotAvailableBookCopies()
        {
            var bookCopiesOnLoan = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null).
                Select(l => l.BookCopy);
            return bookCopiesOnLoan;
        }

        /* ALTERNATIVE SOLUTION to BookCopies on Loan (With inner join).
        /// <summary>
        /// Retrieves all BookCopies currently on loan.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> BookCopiesOnLoan()
        {
            // Gets all loans.
            var loans = _loanRepository.All();

            // Gets all bookcopies.
            var bookCopies = _bookCopyRepository.All();

            // Get all bookcopies on loan with an inner join between loans and bookcopies.
            var bookCopiesOnLoan = loans.Where(l => l.DateTimeOfReturn == null).
                Join(bookCopies,
                l => l.BookCopy.Id,
                bc => bc.Id,
                (l, bc) => bc); // Projects the bookcopies.

            return bookCopiesOnLoan;
        }*/

        /// <summary>
        /// Delegate invocation method for the eventhandler Updated.
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnUpdated(UpdatedEventArgs<BookCopy> args)
        {
            // Checks that at least one delegate has been assigned to the eventhandler. 
            // (Checks whether the eventhandler has any subscribers).
            if (args != null)
            {
                // Invocate delegate (Publish event to subscribers).
                Updated(this, args);
            }
        }

    }
}
