using System;
using System.Collections.Generic;

namespace MediBook.Client.Core.Components.Account.Models
{
    public class RegistrationResponse
    {
        public string Message { get; set; }

        public Dictionary<string, List<string>> ModelState { get; set; }
    }
}
