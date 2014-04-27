using MediBook.Client.Core.Components.Account;

namespace MediBook.Client.Core.Networking
{
    public class AuthGetRequest : GetRequest
    {
        public AuthGetRequest(string requestUrl)
            : base(requestUrl)
        {
            var token = AppCore.Instance.GetComponent<AccountComponent>().Token;

            Request.AddHeader("Authorization", string.Format("{0} {1}", token.TokenType, token.AccessToken));
        }
    }
}
