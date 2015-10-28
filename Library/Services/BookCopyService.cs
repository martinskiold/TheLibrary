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
    public class BookCopyService : IService<UpdatedEventArgs<BookCopy>>
    {
        public event EventHandler<UpdatedEventArgs<BookCopy>> Updated;

        private BookCopyRepository _bookCopyRepository;
        private BookRepository _bookRepository;
        private LoanRepository _loanRepository;
        
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
        /// Retrieves all BookCopies currently on loan.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> BookCopiesOnLoan()
        {
            var loans = _loanRepository.All();
            var bookCopies = _bookCopyRepository.All();
            
            var bookCopiesOnLoan = loans.Where(l => l.DateTimeOfReturn == null).
                Join(bookCopies,
                l => l.BookCopy.Id,
                bc => bc.Id,
                (l, bc) => bc);

            return bookCopiesOnLoan;
        }

        /// <summary>
        /// Retrieves all available BookCopies.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> AvailableBookCopies()
        {
            var bookCopiesOnLoan = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null).
                Select(l => l.BookCopy);
            var bookCopies = _bookCopyRepository.All();
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
