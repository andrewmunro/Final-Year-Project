using System;
using System.Threading.Tasks;

using MediBook.Shared.Config;

using RestSharp;

namespace MediBook.Client.Core.Networking
{
    public class HttpRequest
    {
        public RestClient Client { get; private set; }

        public RestRequest Request { get; private set; }

        public HttpRequest(String requestUrl, Method httpMethod)
        {
            this.Client = new RestClient(Configuration.ServerUrl);
            this.Request = new RestRequest("api/" + requestUrl, httpMethod);

            //Debug
            this.Request.OnBeforeDeserialization = response => Console.WriteLine("Recieved response for request '"+ requestUrl +"':\n" + response.Content);
        }

        public async Task<IRestResponse> Execute()
        {
            return await this.ExecuteAwait();
        }

        public async Task<T> Execute<T>()
        {
            return await this.ExecuteAwait<T>();
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

        public Task<T> ExecuteAwait<T>()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            this.Client.ExecuteAsync<T>(this.Request, (response, asyncHandle) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    taskCompletionSource.SetException(response.ErrorException);
                }
                else
                {
                    taskCompletionSource.SetResult(response.Data);
                }
            });
            return taskCompletionSource.Task;
        }
    }
}
