using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediBook.Server.Exceptions
{
    public class AccountAlreadyExistsException : Exception
    {
        public AccountAlreadyExistsException()
            : base()
        {
            
        }
    }
}