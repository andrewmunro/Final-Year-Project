using MediBook.Client.Core.Networking;

using RestSharp;

namespace MediBook.Client.Core.Components.Account.Requests.Get
{
    public class GetAccount : HttpRequest
    {
        public GetAccount()
            : base("", Method.GET)
        {
            
        }
    }
}
