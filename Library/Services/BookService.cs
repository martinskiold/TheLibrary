//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services {
    public class BookService : IService<UpdatedEventArgs<Book>>
    {
        // Handles events triggered from changes to the library's books.
        public event EventHandler<UpdatedEventArgs<Book>> Updated;

        AuthorRepository _authorRepository;
        BookRepository _bookRepository;
        BookCopyRepository _bookCopyRepository;
        LoanRepository _loanRepository;

        public BookService(RepositoryFactory repoFactory)
        {
            _bookRepository = repoFactory.GetBookRepository();
            _loanRepository = repoFactory.GetLoanRepository();
            _bookCopyRepository = repoFactory.GetBookCopyRepository();
            _authorRepository = repoFactory.GetAuthorRepository();
        }

        /// <summary>
        /// Adds a Book to the library.
        /// </summary>
        /// <param name="item"></param>
        private bool Add(Book item) 
        {
            try
            {
                _bookRepository.Add(item);
               
                // Automatically adds one bookcopy of the book.
                BookCopy bc = new BookCopy()
                {
                    Book = item
                };
                try
                {
                    _bookCopyRepository.Add(bc);
                }
                catch 
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
            UpdatedEventArgs<Book> args = new UpdatedEventArgs<Book>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Adds a Book to the library.
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public bool AddBook(string isbn, string title, string description, int authorId) 
        {
            if (title != "" && description != "" && isbn != "" && title != null && description != null && isbn != null) 
            {
                var author = _authorRepository.Find(authorId);
                if (author != null) 
                {
                    Book b = new Book()
                    {
                        Title = title,
                        ISBN = isbn,
                        Description = description,
                        Author = author
                    };
                    return Add(b);
                }
            }
            return false;
        }

        /// <summary>
        /// Removes a Book from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(Book item) 
        {
            try
            {
                _bookRepository.Remove(item);
                UpdatedEventArgs<Book> args = new UpdatedEventArgs<Book>(item);
                OnUpdated(args);
                return true;
            }
            catch 
            {
                return false;
            }

        }

        /// <summary>
        /// Retrieves a Book from the library.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Find(int id) 
        {
            return _bookRepository.Find(id);
        }

        /// <summary>
        /// Edits a Book from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Edit(Book item) 
        {
            try
            {
                _bookRepository.Edit(item);
                UpdatedEventArgs<Book> args = new UpdatedEventArgs<Book>(item);
                OnUpdated(args);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all Books from the library.
        /// If there are no Books in the library, it returns a default empty list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> All()
        {
            var books = _bookRepository.All();
            return books;
        }

        /// <summary>
        /// Retrieves all BookCopies connected to the book, that are not currently on loan.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public IEnumerable<BookCopy> AvailableCopies(int bookId)
        {
            var book = _bookRepository.Find(bookId);
            if (book == null) 
            {
                return default(IEnumerable<BookCopy>);
            }
            var bookCopies = book.BookCopies;
            if (bookCopies == null) 
            {
                return default(IEnumerable<BookCopy>);
            }
            var copiesOnLoan = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null).
                Select(l => l.BookCopy);

            var availableCopies = Enumerable.Except(bookCopies.ToList(), copiesOnLoan);
            if (!availableCopies.Any()) 
            {
                return default(IEnumerable<BookCopy>);
            }
            return availableCopies;
        }

        /// <summary>
        /// Retrieves all Books that have available copies.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> BooksWithAvailableCopies()
        {
            return _bookRepository.All().Where(b => 
            {
                var availableCopies = AvailableCopies(b.Id);
                if (availableCopies != null) 
                {
                    return availableCopies.Count() > 0;
                }
                return false;
            }
            );
        }

        /// <summary>
        /// Retrieves Books that match the keyword in some way.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> AvailableBooksSearch(string search)
        {
            if (search == "") 
            {
                return All();
            }
            var availableBooks = BooksWithAvailableCopies();
            if (availableBooks == null) 
            {
                return default(IEnumerable<Book>);
            }
            return availableBooks.
                Where(b => 
                    b.Title.Contains(search) || 
                    b.Description.Contains(search) ||
                    b.Author.Name.Contains(search) ||
                    b.ISBN.Contains(search)).
                    OrderBy(b => b.Author.Id).
                    ThenBy(b => b.Title);
        }

        /// <summary>
        /// Retrieves all BookCopies from a book's id.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public IEnumerable<BookCopy> AllCopiesOfBook(int bookId)
        {
            var book = Find(bookId);
            if (book == null) 
            {
                return default(IEnumerable<BookCopy>);
            }
            return book.BookCopies;
        }


        /// <summary>
        /// Delegate invocation method for the eventhandler Updated.
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnUpdated(UpdatedEventArgs<Book> args) 
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
