using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using MediBook.Shared.Models;

namespace MediBook.Testing
{
    public class PushNotification
    {
        private const string ApiKey = "AIzaSyBy1osw9nU2YAzPkJCwLA5BtvlzZBhNxfY";
        private const string RequestMethod = "POST";
        private const string RequestContentType = "application/json";
        private const string GcmUrl = "https://android.googleapis.com/gcm/send";

        private HttpWebRequest Request { get; set; }

        public PushNotification(string title, string body, List<string> senderIDs)
        {
            this.Request = (HttpWebRequest)WebRequest.Create(GcmUrl);
            this.Request.Method = RequestMethod;
            this.Request.KeepAlive = false;
            string postData = "{ \"registration_ids\": [ \"" + String.Join(",", senderIDs.ToArray()) + "\" ], \"data\": {\"title\": \"" + title + "\", \"body\": \"" + body + "\"}}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            this.Request.ContentType = RequestContentType;
            this.Request.Headers.Add(HttpRequestHeader.Authorization, String.Format("key={0}", ApiKey));

            var dataStream = this.Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = this.Request.GetResponse();
            var responseCode = ((HttpWebResponse)response).StatusCode;
            if (responseCode.Equals(HttpStatusCode.Unauthorized) || responseCode.Equals(HttpStatusCode.Forbidden))
            {
                var text = "Unauthorized - need new token";

            }
            else if (!responseCode.Equals(HttpStatusCode.OK))
            {
                var text = "Response from web service isn't OK";
            }

            var reader = new StreamReader(response.GetResponseStream());
            string responseLine = reader.ReadLine();
            reader.Close();
        }
    }
}