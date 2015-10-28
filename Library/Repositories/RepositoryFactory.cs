//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Library.Repositories;
using Library.Models;

namespace Library.Repositories {
    
    /// <summary>
    /// This class handles instantiation of the repositories.
    /// </summary>
    public class RepositoryFactory {
        
        /// <summary>
        /// Wrapper property to get a context instance.
        /// </summary>
        static LibraryContext context 
        {
            get { return ContextSingleton.GetContext(); }
        }

        /// <summary>
        /// Retrieves a BookRepository instance.
        /// </summary>
        /// <returns></returns>
        public BookRepository GetBookRepository() 
        {
            return new BookRepository(context);
        }

        /// <summary>
        /// Retrieves a BookCopyRepository instance.
        /// </summary>
        /// <returns></returns>
        public BookCopyRepository GetBookCopyRepository() 
        {
            return new BookCopyRepository(context);
        }

        /// <summary>
        /// Retrieves an AuthorRepository instance.
        /// </summary>
        /// <returns></returns>
        public AuthorRepository GetAuthorRepository() 
        {
            return new AuthorRepository(context);
        }

        /// <summary>
        /// Retrieves a LoanRepository instane.
        /// </summary>
        /// <returns></returns>
        public LoanRepository GetLoanRepository() 
        {
            return new LoanRepository(context);
        }

        /// <summary>
        /// Retrieves a MemberRepository instance.
        /// </summary>
        /// <returns></returns>
        public MemberRepository GetMemberRepository() 
        {
            return new MemberRepository(context);
        }
    }
}