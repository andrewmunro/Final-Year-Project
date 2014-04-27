using MediBook.Client.Core.Networking;

using RestSharp;

namespace MediBook.Client.Core.Components.Account.Requests.Post
{
    public class AuthPostDeviceID : AuthPostRequest
    {
        public AuthPostDeviceID(string registrationID) : base("Account/DeviceID")
        {
            Request.AddParameter("deviceID", registrationID, ParameterType.QueryString);
        }
    }
}
