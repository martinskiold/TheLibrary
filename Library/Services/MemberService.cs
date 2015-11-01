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
using Library.Repositories;

namespace Library.Services
{
    /// <summary>
    /// Application logic regarding Members.
    /// </summary>
    public class MemberService : IService<UpdatedEventArgs<Member>>
    {
        // Handles events triggered from changes to the library's Members.
        public event EventHandler<UpdatedEventArgs<Member>> Updated;

        // The repositories used by this libraryservice.
        private MemberRepository _memberRepository;
        private LoanRepository _loanRepository;

        /// <summary>
        /// Creates the application logic context necessary for handling the Members of The library.
        /// </summary>
        /// <param name="repoFactory"></param>
        public MemberService(RepositoryFactory repoFactory) 
        {
            _memberRepository = repoFactory.GetMemberRepository();
            _loanRepository = repoFactory.GetLoanRepository();
        }

        /// <summary>
        /// Adds a Member to the library.
        /// </summary>
        /// <param name="item"></param>
        private bool Add(Member item)
        {
            try
            {
                _memberRepository.Add(item);
            }
            catch 
            {
                return false;
            }
            UpdatedEventArgs<Member> args = new UpdatedEventArgs<Member>(item);
            OnUpdated(args);
            return true;
        }

        /// <summary>
        /// Adds a member to the library.
        /// </summary>
        /// <param name="personalId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddMember(int personalId, string name) 
        {
            if (name != "" && name != null) 
            {
                Member newMember = new Member()
                {
                    PersonalId = personalId,
                    Name = name
                };
                return Add(newMember);
            }
            return false;
        }

        /// <summary>
        /// Removes a Member from the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(Member item)
        {
            try
            {
                _memberRepository.Remove(item);
                UpdatedEventArgs<Member> args = new UpdatedEventArgs<Member>(item);
                OnUpdated(args);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves a Member from the library.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Member Find(int id)
        {
            return _memberRepository.Find(id);
        }

        /// <summary>
        /// Edits a Member at the library.
        /// </summary>
        /// <param name="item"></param>
        public bool Edit(Member item)
        {
            try
            {
                _memberRepository.Edit(item);
                UpdatedEventArgs<Member> args = new UpdatedEventArgs<Member>(item);
                OnUpdated(args);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all Members from the library.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> All()
        {
            var members = _memberRepository.All();
            return members;
        }

        /// <summary>
        /// Retrieves the member's loans.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Loan> CurrentLoans(int id)
        {
            var member = _memberRepository.Find(id);
            // If there was no matching member.
            if (member == null) 
            {
                return default(List<Loan>);
            }
            // Gets the member's loans.
            var membersLoans = member.Loans;
            // If the member has no loan.
            if (membersLoans == null) 
            {
                return default(List<Loan>);
            }
            // Return the members current loans.
            return membersLoans.Where(l => l.DateTimeOfReturn == null);
        }

        /// <summary>
        /// Retrieves the member's returned loans.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Loan> ReturnedLoans(int id)
        {
            var member = _memberRepository.Find(id);
            if (member == null) 
            {
                return default(List<Loan>);
            }
            var membersLoans = member.Loans;
            if (membersLoans == null) 
            {
                return default(List<Loan>);
            }
            // Returns the member's loans that has a date of return. (Which means the loan is not active).
            return membersLoans.Where(l => l.DateTimeOfReturn != null);
        }

        /// <summary>
        /// Retrieves the member's loans which is overdue.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Loan> LoansCurrentlyOverdue(int id)
        {
            var member = _memberRepository.Find(id);
            if(member == null)
            {
                return default(List<Loan>);
            }
            var membersLoans = member.Loans;
            if (membersLoans == null)
            {
                return default(List<Loan>);
            }
            // Returns the member's loans that has a duedate that has 
            // been passed and that at the same time doesn't have a definite return date.
            // This means the loan is currently active and overdue.
            return membersLoans.Where(l => DateTime.Now > l.DateTimeDueDate && l.DateTimeOfReturn == null);
        }

        /// <summary>
        /// Delegate invocation method for the eventhandler Updated.
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnUpdated(UpdatedEventArgs<Member> args)
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
