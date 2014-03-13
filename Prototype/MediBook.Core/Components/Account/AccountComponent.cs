using System;

using MediBook.Client.Core.Components.Account.Requests.Post;

namespace MediBook.Client.Core.Components.Account
{
    public class AccountComponent : ComponentBase
    {
        public Models.Account Account { get; private set; }

        public AccountComponent(AppCore core)
            : base(core)
        {
           this.Register("andrew", "password");
        }

        public async void Register(string username, string password)
        {
            var request = new PostRegisterAccount(username, password, password);
            var response = await request.Execute();
            Console.WriteLine(response);
        }
    }
}
