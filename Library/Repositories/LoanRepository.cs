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
    public class LoanRepository : IRepository<Loan, int>
    {

        LibraryContext _context;

        public LoanRepository(LibraryContext ctx) 
        {
            _context = ctx;
        }

        /// <summary>
        /// Adds the Loan to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Loan item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't add (item) to database: ArgumentNullReference (item)");
            }
            _context.Loans.Add(item);

            // Add record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the Loan from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Loan item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't remove (item) from database: ArgumentNullReference (item)");
            }
            _context.Loans.Remove(item);

            // Delete record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the Loan from the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Loan Find(int id)
        {
            Loan result;

            try
            {
                result = _context.Loans.Find(id);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Edits the Loan in the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Loan item)
        {
            try
            {
                // Retrieves the Loan.
                var loan = Find(item.Id);

                if (!NullReference(loan))
                {
                    // Edit the retrieved Loan.
                    loan.DateTimeOfLoan = item.DateTimeOfLoan;
                    loan.DateTimeDueDate = item.DateTimeDueDate;
                    loan.DateTimeOfReturn = item.DateTimeOfReturn;
                    loan.Member = item.Member;
                    loan.BookCopy = item.BookCopy;

                    // Update the record.
                    _context.SaveChanges();        
                }
            }
            catch (NullReferenceException) 
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves all Loans from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> All()
        {
            IEnumerable<Loan> result;
            try
            {
                result = _context.Loans.ToList();
                if (result.Count() == 0)
                {
                    result = default(IEnumerable<Loan>);
                }
            }
            catch
            {
                result = default(IEnumerable<Loan>);
            }
            return result;
        }

        /// <summary>
        /// Checks if the item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool NullReference(Loan item)
        {
            return item == null;
        }
    }
}
