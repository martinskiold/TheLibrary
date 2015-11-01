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
using System.Data.Entity;

namespace Library.Services
{
    /// <summary>
    /// Application logic regarding Authors.
    /// </summary>
    public class AuthorService : IService<UpdatedEventArgs<Author>>
    {
        // Handles events triggered from changes to the library's Authors.
        public event EventHandler<UpdatedEventArgs<Author>> Updated;

        // The repositories used by this libraryservice.
        private AuthorRepository _authorRepository;

        /// <summary>
        /// Creates the application logic context necessary for handling the Authors of The library.
        /// </summary>
        /// <param name="repoFactory"></param>
        public AuthorService(RepositoryFactory repoFactory) 
        {
            _authorRepository = repoFactory.GetAuthorRepository();
        }

        /// <summary>
        /// Adds an Author to the library.
        /// </summary>
        /// <param name="item"></param>
        private bool Add(Author item)
        {
            try
            {
                _authorRepository.Add(item);
            }
            catch
            {
                return false;
            }
            UpdatedEventArgs<Author> args = new UpdatedEventArgs<Author>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Adds an Author to the library.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddAuthor(string name) 
        {
            if (name != "" && name != null) 
            {
                Author author = new Author()
                {
                    Name = name
                };
                return Add(author);
            }
            return false;
        }

        /// <summary>
        /// Removes an Authour from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(Author item)
        {
            try
            {
                _authorRepository.Remove(item);
                UpdatedEventArgs<Author> args = new UpdatedEventArgs<Author>(item);
                OnUpdated(args);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves an Author from the library.
        /// If there is no matching author, null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author Find(int id)
        {
            return _authorRepository.Find(id);
        }

        /// <summary>
        /// Edits an Author in the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Edit(Author item)
        {
            try
            {
                _authorRepository.Edit(item);
                UpdatedEventArgs<Author> args = new UpdatedEventArgs<Author>(item);
                OnUpdated(args);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all Authors from the library.
        /// If there are no authors in the library, it returns a default empty list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Author> All()
        {
            var authors = _authorRepository.All();
            return authors;
        }

        /// <summary>
        /// Returns all books written by the author.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Book> BooksFromAuthor(int id) 
        {
            var author = _authorRepository.Find(id);
            // If there was no author with the given id.
            if(author == null)
            {
                return default(IEnumerable<Book>);
            }
            var authorBooks = author.Books;
            // If the author has no books.
            if (authorBooks == null) 
            {
                return default(IEnumerable<Book>);
            }
            return authorBooks;
        }

        /// <summary>
        /// Delegate invocation method for the eventhandler Updated.
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnUpdated(UpdatedEventArgs<Author> args)
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
