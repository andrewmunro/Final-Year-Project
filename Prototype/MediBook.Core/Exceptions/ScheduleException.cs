using System;
using System.Collections.Generic;

namespace MediBook.Client.Core.Exceptions
{
    public class ScheduleException : Exception
    {
        public ScheduleException(string message) : base(message) { }
    }
}
