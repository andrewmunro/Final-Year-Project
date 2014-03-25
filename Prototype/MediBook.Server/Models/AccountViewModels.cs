using System;
using System.Collections.Generic;

namespace MediBook.Server.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginView
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoView
    {
        public string LocalLoginProvider { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserLoginInfoView> Logins { get; set; }

        public IEnumerable<ExternalLoginView> ExternalLoginProviders { get; set; }
    }

    public class UserInfoView
    {
        public string UserName { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoView
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
