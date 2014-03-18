using MediBook.Client.Core.Networking;

namespace MediBook.Client.Core.Components.Account.Requests.Post
{
    public class PostLoginAccount : PostRequest
    {
        public PostLoginAccount(string username, string password) : base("Token")
        {
            Request.AddParameter("grant_type", "password");
            Request.AddParameter("UserName", username);
            Request.AddParameter("Password", password);
        }
    }
}
