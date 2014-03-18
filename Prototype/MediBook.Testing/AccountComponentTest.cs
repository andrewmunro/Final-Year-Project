using System;
using System.Net;
using System.Threading.Tasks;

using MediBook.Client.Core;
using MediBook.Client.Core.Components.Account;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediBook.Testing
{
    [TestClass]
    public class AccountComponentTest
    {
        private AppCore AppCore { get; set; }
        private AccountComponent AccountComponent { get { return AppCore.GetComponent<AccountComponent>(); } }

        public AccountComponentTest()
        {
            AppCore = new AppCore(false);
            AppCore.AddComponent<AccountComponent>();
        }

        [TestMethod]
        public void AccountComponentNotNull()
        {
            Assert.IsNotNull(AccountComponent);
        }

        [TestMethod]
        public async Task RegisterAccount()
        {
            var username = "testuser" + new Random().Next();
            await AccountComponent.Register(username, "password");
        }

        [TestMethod]
        public async Task LoginAccount()
        {
            var response = await AccountComponent.Login("Andrew", "Password");
            Assert.IsNotNull(response.AccessToken);
            Assert.IsNotNull(response.Expires);
            Assert.IsNotNull(response.ExpiresIn);
            Assert.IsNotNull(response.Issued);
            Assert.IsNotNull(response.TokenType);
            Assert.IsNotNull(response.UserName);
            Assert.IsNotNull(AccountComponent.Token);
        }
    }
}
