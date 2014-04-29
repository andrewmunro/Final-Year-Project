using System;
using System.Collections.Generic;

namespace MediBook.Shared.Models
{
    public class PossibleTime
    {
        public String Time { get; set; }

        public List<Guid> AppointmentsToCancel { get; set; }
    }
}
