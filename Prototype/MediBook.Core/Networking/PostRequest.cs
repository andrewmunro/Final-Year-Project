using RestSharp;

namespace MediBook.Client.Core.Networking
{
    public class PostRequest : HttpRequest
    {
        public PostRequest(string requestUrl)
            : base(requestUrl, Method.POST)
        {
            
        }
    }
}
