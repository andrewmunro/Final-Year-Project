using MediBook.Server.Notification;

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
            //Force initialisation of Notification service.
            var notificationService = NotificationService.Instance;
        }
    }
}
