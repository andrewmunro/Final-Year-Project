using System.Collections.Generic;

namespace MediBook.Shared.Models
{
    public class ScheduleResponse
    {
        public string Message { get; set; }
        public List<PossibleTime> PossibleTimes { get; set; }
    }
}
