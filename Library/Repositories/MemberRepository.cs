//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Library.Models;

namespace Library.Repositories
{
    public class MemberRepository : IRepository<Member, int>
    {

        LibraryContext _context;

        public MemberRepository(LibraryContext ctx) 
        {
            _context = ctx;
        }

        /// <summary>
        /// Adds the Member to the database.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Member item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't add (item) to database: ArgumentNullReference (item)");
            }
            _context.Members.Add(item);

            // Add record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the Member from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Member item)
        {
            if (NullReference(item))
            {
                throw new ArgumentNullException("Can't remove (item) from database: ArgumentNullReference (item)");
            }
            _context.Members.Remove(item);

            // Delete record.
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the Member from the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Member Find(int id)
        {
            Member result;

            try
            {
                result = _context.Members.Find(id);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Edits the Member in the database.
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Member item)
        {
            try
            {
                // Retrieves the Member.
                var member = Find(item.Id);

                if (!NullReference(member))
                {
                    // Edits the retrieved Member.
                    member.Loans = item.Loans;
                    member.Name = item.Name;
                    member.PersonalId = item.PersonalId;

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
        /// Retrieves all Members from the database.
        /// If there are no Members => returns null.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> All()
        {
            IEnumerable<Member> result;
            try
            {
                result = _context.Members.ToList();
                if (result.Count() == 0)
                {
                    result = default(IEnumerable<Member>);
                }
            }
            catch
            {
                result = default(IEnumerable<Member>);
            }
            return result;
        }

        /// <summary>
        /// Checks if the item is null.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool NullReference(Member item)
        {
            return item == null;
        }
    }
}
