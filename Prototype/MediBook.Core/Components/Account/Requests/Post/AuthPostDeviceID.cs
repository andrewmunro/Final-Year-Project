using MediBook.Client.Core.Networking;

namespace MediBook.Client.Core.Components.Account.Requests.Post
{
    public class AuthPostDeviceID : AuthPostRequest
    {
        public AuthPostDeviceID(AppCore core, string registrationID) : base("DeviceID", core)
        {
            Request.AddParameter("deviceID", registrationID);
        }
    }
}
