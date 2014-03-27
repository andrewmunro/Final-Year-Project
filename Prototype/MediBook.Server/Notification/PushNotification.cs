using System;
using System.IO;
using System.Net;
using System.Text;

using MediBook.Shared.Models;

namespace MediBook.Server.Notification
{
    public class PushNotification
    {
        private const string ApiKey = "AIzaSyBy1osw9nU2YAzPkJCwLA5BtvlzZBhNxfY";
        private const string RequestMethod = "POST";
        private const string RequestContentType = "application/json";
        private const string GcmUrl = "https://android.googleapis.com/gcm/send";

        private HttpWebRequest Request { get; set; }

        public PushNotification(NotificationModel notification)
        {
            Request = (HttpWebRequest)WebRequest.Create(GcmUrl);
            Request.Method = RequestMethod;
            Request.KeepAlive = false;
            string postData = "{ \"registration_ids\": [ \"" + notification.Appointment.Patient.DeviceID + "\" ], \"data\": {\"title\": \"" + notification.Title + "\", \"body\": \"" + notification.Body + "\"}}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            Request.ContentType = RequestContentType;
            Request.Headers.Add(HttpRequestHeader.Authorization, String.Format("key={0}", ApiKey));

            var dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = Request.GetResponse();
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