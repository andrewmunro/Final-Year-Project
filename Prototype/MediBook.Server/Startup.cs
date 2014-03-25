using System.Data.Entity;
using System.Web.Mvc;

using MediBook.Server.Models;

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MediBook.Server.Startup))]

namespace MediBook.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureRoles();
        }
    }
}
