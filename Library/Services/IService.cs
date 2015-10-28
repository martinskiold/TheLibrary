//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Repositories;

namespace Library.Services {
    
    /// <summary>
    /// Allows services to notify when their
    /// underlying data model changes.
    /// </summary>
    interface IService<T> {
        event EventHandler<T> Updated;
    }
}
