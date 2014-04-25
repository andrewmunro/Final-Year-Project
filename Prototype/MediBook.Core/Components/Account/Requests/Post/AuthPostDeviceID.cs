using MediBook.Client.Core.Networking;

using RestSharp;

namespace MediBook.Client.Core.Components.Account.Requests.Post
{
    public class AuthPostDeviceID : AuthPostRequest
    {
        public AuthPostDeviceID(AppCore core, string registrationID) : base("Account/DeviceID", core)
        {
            Request.AddParameter("deviceID", registrationID, ParameterType.QueryString);
        }
    }
}
