//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models {
    public class Book {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
       
        // 1-1 relationship. Book has mandatory participation. Author is parent. [Foreign key in Book]
        [Required]
        public virtual Author Author { get; set; }
        
        // 1-* relationship. Book has optional participation. Book is Parent. [Foreign key in BookCopies]
        public virtual ICollection<BookCopy> BookCopies { get; set; }

        /// <summary>
        /// String reprentation of a Book.
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            return String.Format("[{0}] {1}", this.Id, this.Title);
        }
    }
}