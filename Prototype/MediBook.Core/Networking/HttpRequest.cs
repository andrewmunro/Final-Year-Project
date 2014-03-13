using System;
using System.Threading.Tasks;

using RestSharp;

namespace MediBook.Client.Core.Networking
{
    public class HttpRequest
    {
        public RestClient Client { get; private set; }

        public RestRequest Request { get; private set; }

        public HttpRequest(String requestUrl, Method httpMethod)
        {
            this.Client = new RestClient("http://10.0.0.197:1337/");
            this.Request = new RestRequest("api/" + requestUrl, httpMethod);
        }

        public async Task<IRestResponse> Execute()
        {
            return await this.ExecuteAwait();
        }

        public Task<IRestResponse> ExecuteAwait()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            this.Client.ExecuteAsync(this.Request, (response, asyncHandle) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    taskCompletionSource.SetException(response.ErrorException);
                }
                else
                {
                    taskCompletionSource.SetResult(response);
                }
            });
            return taskCompletionSource.Task;
        }
    }
}
