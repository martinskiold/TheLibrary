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

namespace Library.Services
{
    /// <summary>
    /// Application logic regarding Loans.
    /// </summary>
    public class LoanService : IService<UpdatedEventArgs<Loan>>
    {
        // Handles events triggered from changes to the library's Loans.
        public event EventHandler<UpdatedEventArgs<Loan>> Updated;

        // The repositories used by this libraryservice.
        LoanRepository _loanRepository;
        BookCopyRepository _bookCopyRepository;
        MemberRepository _memberRepository;
        BookRepository _bookRepository;

        /// <summary>
        /// Creates the application logic context necessary for handling the Loans of The library.
        /// </summary>
        /// <param name="repoFactory"></param>
        public LoanService(RepositoryFactory repoFactory) 
        {
            _loanRepository = repoFactory.GetLoanRepository();
            _bookCopyRepository = repoFactory.GetBookCopyRepository();
            _memberRepository = repoFactory.GetMemberRepository();
            _bookRepository = repoFactory.GetBookRepository();
        }

        /// <summary>
        /// Creates a loan on the given member with the given book.
        /// </summary>
        /// <returns></returns>
        public bool AddLoan(int bookId, int memberId)
        {
            var member = _memberRepository.Find(memberId);
            var book = _bookRepository.Find(bookId);
            if (member != null && book != null)
            {
                // Check if there is any available BookCopies.
                var copies = _bookRepository.All().Where(b => b.Id == book.Id).SelectMany(b => b.BookCopies);
                var copiesOnLoan = _loanRepository.All().Where(l => l.DateTimeOfReturn == null).Select(l => l.BookCopy);
                var availableBookCopies = Enumerable.Except(copies, copiesOnLoan);

                // If there is any available BookCopies of the book.
                if (availableBookCopies.Count() > 0)
                {
                    // Takes the first available BookCopy.
                    BookCopy copy = availableBookCopies.ElementAt(0);
                    // Creates a loan with the available BookCopy.
                    Loan loan = new Loan()
                    {
                        DateTimeOfLoan = DateTime.Now,
                        DateTimeDueDate = DateTime.Now.AddDays(15),
                        BookCopy = copy,
                        Member = member
                    };
                    return Add(loan);
                }
                // There is no available BookCopy of the book and false is therefor returned.
            }
            return false;
        }


        /// <summary>
        /// Adds a Loan to a member at the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Add(Loan item)
        {
            try
            {
                _loanRepository.Add(item);
            }
            catch 
            {
                return false;
            }
            UpdatedEventArgs<Loan> args = new UpdatedEventArgs<Loan>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Removes a Loan from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(Loan item)
        {
            try
            {
                _loanRepository.Remove(item);
                UpdatedEventArgs<Loan> args = new UpdatedEventArgs<Loan>(item);
                OnUpdated(args);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves a Loan at the library.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Loan Find(int id)
        {
            return _loanRepository.Find(id);
        }

        /// <summary>
        /// Edits a Loan.
        /// </summary>
        /// <param name="item"></param>
        public bool Edit(Loan item)
        {
            try
            {
                _loanRepository.Edit(item);
            }
            catch
            {
                return false;
            }
            UpdatedEventArgs<Loan> args = new UpdatedEventArgs<Loan>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Returns the book from a loan.
        /// If DueDate has passed => the method returns a fine,
        /// calculated on passed days since DueDate, in Swedish Kronor.
        /// If DueDate has not passed => the method returns 0 as in 0 Swedish Kronor (No Fine).
        /// </summary>
        /// <param name="loanId"></param>
        public int ReturnBook(int loanId)
        {
            int fine = 0;
            var loan = Find(loanId);

            // If there is a matching loan.
            if (loan != null)
            {
                // If duedate has passed.
                if (DateTime.Now > loan.DateTimeDueDate)
                {
                    // Calculate fine.
                    fine = CalculateOverdueFine(loan.Id);
                }
                // Edit the loan so that it is marked as returned.
                loan.DateTimeOfReturn = DateTime.Now;
                // Edit the record in the database.
                Edit(loan);
            }
            // Return the amount of fine. If 0 is returned, the book has been correctly returned before the duedate.
            return fine;
        }

        /// <summary>
        /// Calculates the overdue fine in Swedish Kronor. 10 kr per day.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public int CalculateOverdueFine(int loanId)
        {
            var loan = _loanRepository.Find(loanId);

            // If there is a matching loan.
            if (loan == null)
            {
                // If there is no matching loan, there is nothing to base the fine on.
                return 0;
            }

            // Get the timespan between now and the date that the book passed its duedate.
            TimeSpan span = DateTime.Now.Subtract(loan.DateTimeDueDate);

            // 10 kr for each day overdue.
            return span.Days * 10;
        }

        /// <summary>
        /// Add months overdue to the DueDate.
        /// To fake an overdue, use negative int as argument for months.
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="months"></param>
        public void AddMonthsOverdueToDueDate(int loanId, int months)
        {
            var loan = Find(loanId);

            // If there is a matching loan.
            if (loan != null)
            {
                // NOT NEEDED. Eliminates current duedate if the duedate hasn't passed.
                if (loan.DateTimeDueDate > DateTime.Now)
                {
                    loan.DateTimeDueDate = DateTime.Now;
                }

                // Add months to the duedate.
                loan.DateTimeDueDate = loan.DateTimeDueDate.AddMonths(months);

                // Edits the database record.
                Edit(loan);
            }
        }

        /// <summary>
        /// Retrieves all Loans at the library.
        /// If there are no Loans at the library, it returns a default empty list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> All()
        {
            var loans = _loanRepository.All();
            return loans;
        }

        /// <summary>
        /// Retrieves all current Loans at the library.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> AllCurrent()
        {
            // Retrieves all loans currently active. (Not returned).
            var currentLoans = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn == null);
            return currentLoans;
        }

        /// <summary>
        /// Retrieves all returned Loans at the library.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> AllReturned()
        {
            // Retrieves all returned loans.
            var loansReturned = _loanRepository.
                All().
                Where(l => l.DateTimeOfReturn != null);
            return loansReturned;
        }

        /// <summary>
        /// Retrieves all active Loans that are currently overdue at the library.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Loan> AllCurrentlyOverdue()
        {
            // Retrieves all loans currently overdue and active.
            var currentlyOverdue = _loanRepository.
                All().
                Where(l => DateTime.Now > l.DateTimeDueDate && l.DateTimeOfReturn == null);
            return currentlyOverdue;
        }

        /// <summary>
        /// Delegate invocation method for the eventhandler Updated.
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnUpdated(UpdatedEventArgs<Loan> args)
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
