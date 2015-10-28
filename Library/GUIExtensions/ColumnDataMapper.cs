//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.GUIExtensions
{
    /// <summary>
    /// Creates and maps columndata to a matrix corresponding to a certain listview structure.
    /// ListView structure == (Order, Content and Number of Columns in a ListView). 
    /// This mapping is critical for the ListViewPrinter to work as intended.
    /// </summary>
    public static class ColumnDataMapper
    {
        //
        // If structure of target datarepresentator changes => 
        // make corresponding changes to the creation of columndata in this 
        // class to represent the new columns of the datarepresentator.
        // ...Or...
        // Add methods to create custom columndata.


        // Maps to the standard book ListView.

        public static List<string[]> StandardBookColumnDataMatrix(IEnumerable<Book> books)
        {
            if (books == null)
            {
                // Handles ArgumentNullReference.
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<Book> preventDeferredExecutionOnBooks = books;
                foreach (Book b in preventDeferredExecutionOnBooks)
                {
                    rows.Add(
                        new string[]
                    {                        
                        b.Id.ToString(),
                        b.ISBN,
                        b.Title,
                        b.Description,
                        b.Author.Name
                    });
                }
                return rows;
            }
        }

        // Maps to the standard BookCopy ListView.

        public static List<string[]> StandardBookCopyColumnDataMatrix(IEnumerable<BookCopy> bookCopies)
        {
            if (bookCopies == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<BookCopy> preventDeferredExecutionOnBookCopies = bookCopies;
                foreach (BookCopy bc in preventDeferredExecutionOnBookCopies)
                {
                    rows.Add(
                        new string[]
                    {                        
                        bc.Id.ToString(),
                        bc.Book.Title
                    });
                }
                return rows;
            }
        }

        // Maps to the standard Authors ListView.

        public static List<string[]> StandardAuthorColumnDataMatrix(IEnumerable<Author> authors)
        {
            if (authors == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<Author> preventDeferredExecutionOnAuthors = authors;
                foreach (Author a in preventDeferredExecutionOnAuthors)
                {
                    rows.Add(
                        new string[]
                    {     
                        a.Id.ToString(),
                        a.Name
                    });
                }
                return rows;
            }
        }

        // Maps to the standard Members ListView.

        public static List<string[]> StandardMemberColumnDataMatrix(IEnumerable<Member> members)
        {
            if (members == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<Member> preventDeferredExecutionOnMembers = members;
                foreach (Member m in preventDeferredExecutionOnMembers)
                {
                    rows.Add(
                        new string[]
                    {                        
                        m.Id.ToString(),
                        m.PersonalId.ToString(),
                        m.Name
                    });
                }
                return rows;
            }
        }

        // Maps to the standard Loans ListView.

        public static List<string[]> StandardLoanColumnDataMatrix(IEnumerable<Loan> loans)
        {
            if (loans == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<Loan> preventDeferredExecutionOnLoans = loans;
                foreach (Loan l in preventDeferredExecutionOnLoans)
                {
                    rows.Add(
                        new string[]
                    { 
                        l.Id.ToString(),
                        l.Member.Id.ToString(),
                        l.DateTimeOfLoan.ToString(),
                        l.DateTimeOfReturn.ToString(),
                        l.DateTimeDueDate.ToString(),
                        l.BookCopy.ToString()
                    });
                }
                return rows;
            }
        }



        /* Custom Columndata Mapping
         * 
         * ..Map to specific ListView structures in the custom methods below..
         * 
         */

        /* Maps to a compact version of Book ColumnData */

        public static List<string[]> CompactBookColumnDataMatrix(IEnumerable<Book> books)
        {
            
            if (books == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<Book> preventDeferredExecutionOnBooks = books;
                foreach (Book b in preventDeferredExecutionOnBooks)
                {
                    rows.Add(
                        new string[]
                    {                        
                        b.Id.ToString(),
                        b.Title
                    });
                }
                return rows;
            }

        }

        /* Maps to a listview with BookCopyId and some bookinformation */

        public static List<string[]> BookCopyIncludingBookColumnDataMatrix(IEnumerable<BookCopy> bookCopies)
        {

            if (bookCopies == null)
            {
                return default(List<string[]>);
            }
            else
            {
                List<string[]> rows = new List<string[]>();
                IEnumerable<BookCopy> preventDeferredExecutionOnBookCopies = bookCopies;
                foreach (BookCopy bc in preventDeferredExecutionOnBookCopies)
                {
                    rows.Add(
                        new string[]
                    {                        
                        bc.ToString(),
                        bc.Book.ISBN,
                        bc.Book.Description,
                        bc.Book.Author.ToString()
                    });
                }
                return rows;
            }

        }

    }
}
