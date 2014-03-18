using System;

namespace MediBook.Client.Core.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message)
        : base(message) { }
    }
}
