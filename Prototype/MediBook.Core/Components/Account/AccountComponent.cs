using System.Threading.Tasks;

using MediBook.Client.Core.Components.Account.Models;
using MediBook.Client.Core.Components.Account.Requests.Post;
using MediBook.Client.Core.Exceptions;

namespace MediBook.Client.Core.Components.Account
{
    public class AccountComponent : ComponentBase
    {
        public TokenResponse Token { get; private set; }

        public AccountComponent(AppCore core)
            : base(core)
        {
        }

        public async Task Register(string username, string password)
        {
            var request = new PostRegisterAccount(username, password, password);
            var response = await request.Execute<RegistrationResponse>();

            if (response != null)
            {
                throw new RegistrationException(response.ModelState);
            }
        }

        public async Task<TokenResponse> Login(string username, string password)
        {
            var request = new PostLoginAccount(username, password);
            var response = await request.Execute<TokenResponse>();

            if (response.ErrorDescription != null)
            {
                throw new AuthException(response.ErrorDescription);
            }

            return this.Token = response;
        }
    }
}
