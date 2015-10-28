//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories
{
    public class BookCopyRepository : IRepository<BookCopy, int>
    {

        LibraryContext _context;

        public BookCopyRepository(LibraryContext ctx) 
        {
            _context = ctx;
        }

        /// <summary>
        /// Adds the BookCopy to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(BookCopy item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't add (item) to database: ArgumentNullReference (item)");
            }
            _context.BookCopies.Add(item);

            // Add record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the BookCopy from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(BookCopy item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't remove (item) from database: ArgumentNullReference (item)");
            }
            _context.BookCopies.Remove(item);

            // Delete record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the BookCopy from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookCopy Find(int id)
        {
            BookCopy result;

            try
            {
                result = _context.BookCopies.Find(id);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Edits the BookCopy in the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(BookCopy item)
        {
            try
            {
                // Retrieves the BookCopy.
                var bookCopy = Find(item.Id);

                if (!NullReference(bookCopy))
                {
                    // Edits the retrieved BookCopy.
                    bookCopy.Book = item.Book;

                    // Updates the record.
                    _context.SaveChanges();
                }
            }
            catch (NullReferenceException) 
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves all BookCopies from the database.
        /// If there are no BookCopies => returns null.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCopy> All()
        {
            IEnumerable<BookCopy> result;
            try
            {
                result = _context.BookCopies.ToList();
                if (result.Count() == 0)
                {
                    result = default(IEnumerable<BookCopy>);
                }
            }
            catch
            {
                result = default(IEnumerable<BookCopy>);
            }
            return result;
        }

        /// <summary>
        /// Checks if the item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool NullReference(BookCopy item)
        {
            return item == null;
        }


    }
}
