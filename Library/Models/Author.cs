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
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        // 1-* relationship. Author has optional participation. Author is parent. [Foreign Key in Book]
        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// String representation of an Author.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}] {1}", this.Id, this.Name);
        }
    }
}
