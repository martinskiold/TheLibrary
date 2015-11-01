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

    /// <summary>
    /// Application logic regarding Books.
    /// </summary>
    public class BookService : IService<UpdatedEventArgs<Book>>
    {
        // Handles events triggered from changes to the library's Books.
        public event EventHandler<UpdatedEventArgs<Book>> Updated;

        // The repositories used by this libraryservice.
        AuthorRepository _authorRepository;
        BookRepository _bookRepository;
        BookCopyRepository _bookCopyRepository;
        LoanRepository _loanRepository;

        /// <summary>
        /// Creates the application logic context necessary for handling the Books of The library.
        /// </summary>
        /// <param name="repoFactory"></param>
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
               
                // Automatically adds one bookcopy of the book when the book is added to the database.
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
            // If there is no book with the specified id.
            if (book == null) 
            {
                return default(IEnumerable<BookCopy>);
            }
            var bookCopies = book.BookCopies;
            // If the book has no bookcopies.
            if (bookCopies == null) 
            {
                return default(IEnumerable<BookCopy>);
            }
            
            // Gets all bookcopies currently on loan.
            var copiesOnLoan = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null).
                Select(l => l.BookCopy);

            // (All bookcopies of the book - all bookcopies currently on loan == available bookcopies of the book).
            var availableCopies = Enumerable.Except(bookCopies.ToList(), copiesOnLoan);
            // If there is no available bookcopies of the book.
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
            // Retrieves all Books with available copies.
            return _bookRepository.All().Where(b => 
            {
                var availableCopies = AvailableCopies(b.Id);
                if (availableCopies != null) 
                {
                    // If there is more than 0 BookCopies, return true.
                    return availableCopies.Count() > 0;
                }
                // If there is no available copies.
                return false;
            }
            );
        }

        /// <summary>
        /// Retrieves Books that match the search-keyword in some way.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> AvailableBooksSearch(string search)
        {
            // If search is empty.
            if (search == "") 
            {
                // Get all available books.
                var allAvailable = BooksWithAvailableCopies();
                if (allAvailable == null)
                {
                    return default(IEnumerable<Book>);
                }
                return allAvailable;
            }
            // Make the search case-insensitive.
            search = search.ToLower();
            // Get all available books.
            var availableBooks = BooksWithAvailableCopies();
            if (availableBooks == null) 
            {
                return default(IEnumerable<Book>);
            }
            // Return books that match any of the bookrecord's columns in the database.
            // And then order it by the author's name and then by the book's title.
            return availableBooks.
                Where(b =>
                    b.Title.ToLower().Contains(search) ||
                    b.Description.ToLower().Contains(search) ||
                    b.Author.Name.ToLower().Contains(search) ||
                    b.ISBN.ToLower().Contains(search)).
                    OrderBy(b => b.Author.Name).
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
            // If there was no matching book.
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
