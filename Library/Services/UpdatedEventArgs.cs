//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UpdatedEventArgs<T> : System.EventArgs
    {
        public T Item { get; set; }

        public UpdatedEventArgs(T item) : base()
        {
            Item = item;
        }

    }
}
