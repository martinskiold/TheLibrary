//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models {
    /// <summary>
    /// Derived database strategy
    /// </summary>
    class LibraryDbInit : DropCreateDatabaseAlways<LibraryContext> {
        protected override void Seed(LibraryContext context) {
            base.Seed(context);

            // Initializing example data.
            Author author = new Author()
            {
                Name = "Alexandre Dumas"
            };
            Author author2 = new Author()
            {
                Name = "Ian Fleming"
            };
            Book monteCristo = new Book() 
            {
                Title = "The Count of Monte Cristo",
                ISBN = "1231-21312312",
                Description = "There was a count that...",
                Author = author
            };
            Book bond1 = new Book()
            {
                Title = "James Bond: Casino Royale",
                ISBN = "123123123-123",
                Description = "The agent goes into...",
                Author = author2

            };
            Book bond2 = new Book()
            {
                Title = "James Bond: For Your Eyes Only",
                ISBN = "59382-322422",
                Description = "James bond chases...",
                Author = author2
            };

            BookCopy bc1 = new BookCopy()
            {
                Book = monteCristo
            };

            BookCopy bc2 = new BookCopy()
            {
                Book = monteCristo
            };

            BookCopy bc3 = new BookCopy()
            {
                Book = bond1
            };

            BookCopy bc4 = new BookCopy()
            {
                Book = bond1
            };

            BookCopy bc5 = new BookCopy()
            {
                Book = bond2
            };

            BookCopy bc6 = new BookCopy()
            {
                Book = bond2
            };
            BookCopy bc7 = new BookCopy()
            {
                Book = bond2
            };
            BookCopy bc8 = new BookCopy()
            {
                Book = bond2
            };

            Member m = new Member()
            {
                PersonalId = 910919,
                Name = "Martin Skiold"
            };

            Member m2 = new Member()
            {
                PersonalId = 920524,
                Name = "Gustaf"
            };

            Loan l = new Loan()
            {
                DateTimeOfLoan = DateTime.Now,
                DateTimeDueDate = DateTime.Now.AddDays(15),
                BookCopy = bc1,
                Member = m                
            };

            Loan l2 = new Loan()
            {
                DateTimeOfLoan = DateTime.Now.AddDays(-30),
                DateTimeDueDate = DateTime.Now.AddDays(-15),
                DateTimeOfReturn = DateTime.Now.AddDays(-2),
                Member = m2,
                BookCopy = bc7
            };

            Loan l3 = new Loan()
            {
                DateTimeOfLoan = DateTime.Now,
                DateTimeDueDate = DateTime.Now.AddDays(15),
                Member = m2,
                BookCopy = bc6
            };

            Loan l4 = new Loan()
            {
                DateTimeOfLoan = DateTime.Now,
                DateTimeDueDate = DateTime.Now.AddDays(15),
                Member = m2,
                BookCopy = bc8
            };

            // Add the book to the DbSet of books.
            context.Authors.Add(author);
            context.Authors.Add(author2);
            context.Books.Add(monteCristo);
            context.Books.Add(bond1);
            context.Books.Add(bond2);
            context.BookCopies.Add(bc1);
            context.BookCopies.Add(bc2);
            context.BookCopies.Add(bc3);
            context.BookCopies.Add(bc4);
            context.BookCopies.Add(bc5);
            context.BookCopies.Add(bc6);
            context.BookCopies.Add(bc7);
            context.BookCopies.Add(bc7);
            context.Members.Add(m);
            context.Members.Add(m2);
            context.Loans.Add(l);
            context.Loans.Add(l2);
            context.Loans.Add(l3);
            context.Loans.Add(l4);

            // Persist changes to the database
            context.SaveChanges();
        }
    }
}
