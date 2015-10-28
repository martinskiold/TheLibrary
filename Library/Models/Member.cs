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
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PersonalId { get; set; }
        public string Name { get; set; }

        // 1-* relationship. Member has optional participation. Member is parent. [Foreign key in Loan]
        public virtual ICollection<Loan> Loans { get; set; }

        /// <summary>
        /// String representation of a Member.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.Id, this.Name);
        }
    }
}
