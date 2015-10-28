//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTimeOfLoan { get; set; }
        public DateTime DateTimeDueDate { get; set; }
        public DateTime? DateTimeOfReturn { get; set; }

        // 1-1 relationship. Loan has mandatory participation. BookCopy is Parent. [Foreign key in Loan]
        [Required]
        public virtual BookCopy BookCopy { get; set; }

        // 1-1 relationship. Loan has mandatory participation. Member is Parent. [Foreign key in Loan]
        [Required]
        public virtual Member Member { get; set; }

        /// <summary>
        /// String representation of a Loan.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", this.Id, this.Member.Name, this.BookCopy.Book.Title);
        }
    }
}
