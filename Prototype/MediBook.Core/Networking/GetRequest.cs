using RestSharp;

namespace MediBook.Client.Core.Networking
{
    public class GetRequest : HttpRequest
    {
        public GetRequest(string requestUrl)
            : base(requestUrl, Method.GET)
        {
            
        }
    }
}
