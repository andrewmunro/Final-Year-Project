using System;
using System.Net;
using System.Threading.Tasks;

using MediBook.Client.Core.Components.Account.Models;
using MediBook.Client.Core.Components.Account.Requests.Post;

using RestSharp;

namespace MediBook.Client.Core.Components.Account
{
    public class AccountComponent : ComponentBase
    {
        public Token Token { get; private set; }

        public AccountComponent(AppCore core)
            : base(core)
        {
        }

        public async Task<IRestResponse> Register(string username, string password)
        {
            var request = new PostRegisterAccount(username, password, password);
            var response = await request.Execute();
            return response;
        }

        public async Task<Token> Login(string username, string password)
        {
            var request = new PostLoginAccount(username, password);
            var response = await request.Execute<Token>();
            return this.Token = response;
        }
    }
}
