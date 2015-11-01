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
    /// <summary>
    /// Event arguments for an updated record in the Database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdatedEventArgs<T> : System.EventArgs
    {
        // The item that has been updated in the database.
        public T Item { get; set; }

        /// <summary>
        /// Creates eventarguments with the specified item that has been changed.
        /// </summary>
        /// <param name="item"></param>
        public UpdatedEventArgs(T item) : base()
        {
            Item = item;
        }

    }
}
