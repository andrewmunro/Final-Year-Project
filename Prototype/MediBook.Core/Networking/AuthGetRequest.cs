using MediBook.Client.Core.Components.Account;

namespace MediBook.Client.Core.Networking
{
    class AuthGetRequest : GetRequest
    {
        public AuthGetRequest(string requestUrl, AppCore core)
            : base(requestUrl)
        {
            var token = core.GetComponent<AccountComponent>().Token;

            Request.AddParameter("Authorization", string.Format("{0} {1}", token.TokenType, token.AccessToken));
        }
    }
}
