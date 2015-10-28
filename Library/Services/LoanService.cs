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
    public class LoanService : IService<UpdatedEventArgs<Loan>>
    {
        public event EventHandler<UpdatedEventArgs<Loan>> Updated;

        LoanRepository _loanRepository;
        BookCopyRepository _bookCopyRepository;
        MemberRepository _memberRepository;
        BookRepository _bookRepository;

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

                if (availableBookCopies.Count() > 0)
                {
                    // Takes the first available book.
                    BookCopy copy = availableBookCopies.ElementAt(0);
                    Loan loan = new Loan()
                    {
                        DateTimeOfLoan = DateTime.Now,
                        DateTimeDueDate = DateTime.Now.AddDays(15),
                        BookCopy = copy,
                        Member = member
                    };
                    return Add(loan);
                }
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
            if (loan != null)
            {
                TimeSpan span = DateTime.Now.Subtract(loan.DateTimeDueDate);
                if (DateTime.Now > loan.DateTimeDueDate)
                {
                    fine = CalculateOverdueFine(loan.Id);
                }
                loan.DateTimeOfReturn = DateTime.Now;
                Edit(loan);
            }
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
            if (loan == null)
            {
                return 0;
            }

            TimeSpan span = DateTime.Now.Subtract(loan.DateTimeDueDate);

            // 10 kr for each day overdue.
            return span.Days * 10;
        }

        /// <summary>
        /// Changes days to the loans duedate.
        /// To fake an overdue, use negative int as param="days"
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="days"></param>
        public void ChangeDaysToDueDate(int loanId, int days)
        {
            var loan = Find(loanId);
            if (loan != null)
            {
                loan.DateTimeDueDate = loan.DateTimeDueDate.AddDays(days);
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
