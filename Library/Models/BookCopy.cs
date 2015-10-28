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
    public class BookCopy
    {
        [Key]
        public int Id { get; set; }
        
        // 1-1 relationship. BookCopy has mandatory participation. Book is Parent. [Foreign key in BookCopy]
        [Required]
        public virtual Book Book { get; set; }

        /// <summary>
        /// String representation of a BookCopy.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}], {1}", this.Id, this.Book.Title);
        }
    }
}
