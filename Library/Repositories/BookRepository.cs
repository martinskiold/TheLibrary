//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Library.Models;

namespace Library.Repositories {
    public class BookRepository : IRepository<Book, int> {
        
        // The database session.
        LibraryContext _context;

        public BookRepository(LibraryContext ctx) {
            _context = ctx;
        }

        /// <summary>
        /// Adds the book to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Book item) 
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't add (item) to database: ArgumentNullReference (item)");
            }
            _context.Books.Add(item);

            // Add record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the book from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Book item) 
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't remove (item) from database: ArgumentNullReference (item)");
            }
            _context.Books.Remove(item);

            // Delete record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the book from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Find(int id) 
        {
            Book result;

            try
            {
                result = _context.Books.Find(id);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Edits the book in the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Book item) 
        {       
            try
            {
                // Retrieves the book.
                var book = Find(item.Id);

                if (!NullReference(book)) 
                {
                    // Edits the retrieved Book.
                    book.Title = item.Title;
                    book.ISBN = item.ISBN;
                    book.Description = item.Description;
                    book.Author = item.Author;
                    book.BookCopies = item.BookCopies;

                    // Updates the record.
                    _context.SaveChanges();
                }

            }catch(NullReferenceException)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Retrieves all Books from the database.
        /// If there are no Books => returns null.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> All() 
        {
            IEnumerable<Book> result;
            try
            {
                result = _context.Books.ToList();
                if (result.Count() == 0) 
                {
                    result = default(IEnumerable<Book>);
                }
            }
            catch
            {
                result = default(IEnumerable<Book>);
            }
            return result;
        }

        /// <summary>
        /// Checks if the item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool NullReference(Book item) 
        {
            return item == null;
        }
    }
}