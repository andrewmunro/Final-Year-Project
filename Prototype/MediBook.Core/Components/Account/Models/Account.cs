﻿using System;

namespace MediBook.Client.Core.Components.Account.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }
}
