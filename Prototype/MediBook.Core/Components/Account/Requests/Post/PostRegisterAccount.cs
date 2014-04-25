using MediBook.Client.Core.Networking;

namespace MediBook.Client.Core.Components.Account.Requests.Post
{
    public class PostRegisterAccount : PostRequest
    {
        public PostRegisterAccount(string username, string password, string confirmpassword) : base("Account/Register")
        {
            Request.AddParameter("UserName", username);
            Request.AddParameter("Password", password);
            Request.AddParameter("ConfirmPassword", confirmpassword);
            Request.AddParameter("AccountType", "Patient");
            Request.AddParameter("FirstName", "Testing");
            Request.AddParameter("LastName", "Patient");
        }
    }
}
