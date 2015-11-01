//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories
{
    public class AuthorRepository : IRepository<Author, int>
    {
        // The database session.
        LibraryContext _context;

        public AuthorRepository(LibraryContext ctx) 
        {
            _context = ctx;
        }

        /// <summary>
        /// Adds the Author to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Author item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't add (item) to database: ArgumentNullReference (item)");
            }
            
            // Add item to the database.
            _context.Authors.Add(item);

            // Add record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the Author from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Author item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't remove (item) from database: ArgumentNullReference (item)");
            }
            
            // Tries to remove the item from the databse.
            _context.Authors.Remove(item);

            // Delete record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the Author from the database. 
        /// If there is no matching author, null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Author Find(int id)
        {
            Author result;

            try
            {
                result = _context.Authors.Find(id);
            }
            catch
            {
                // Catches the exception thrown when trying to find an invalid record in the Database.
                // I do this so that I can make sure the find-operation didn't work.
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Edits the Author in the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Author item)
        {
            try
            {
                // Retrieves the Author.
                var author = Find(item.Id);
                if (!NullReference(author))
                {
                    // Edits the retrieved Author.
                    author.Name = item.Name;
                    author.Books = item.Books;

                    // Updates the record.
                    _context.SaveChanges();
                }
            }
            catch (NullReferenceException) 
            {
                // Rethrows Nullreference exception.
                throw;
            }
        }

        /// <summary>
        /// Retrieves all Authors from the database.
        /// If there are no Authors => returns null.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Author> All()
        {
            IEnumerable<Author> result;
            try
            {
                result = _context.Authors.ToList();
                if (result.Count() == 0)
                {
                    result = default(IEnumerable<Author>);
                }
            }
            catch
            {
                result = default(IEnumerable<Author>);
            }
            return result;
        }
        
        /// <summary>
        /// Checks if the item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool NullReference(Author item)
        {
            return item == null;
        }
    }
}
