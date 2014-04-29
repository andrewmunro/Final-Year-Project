using System;
using System.Collections.Generic;

namespace MediBook.Client.Core.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(string message) : base(message) { }
    }
}
