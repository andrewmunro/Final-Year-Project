using MediBook.Server.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MediBook.Server
{
	public partial class Startup
	{
        public RoleManager<IdentityRole> RoleManager { get; private set; }

	    public void ConfigureRoles()
	    {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DataContext()));

	        AddRole("Doctor");
	        AddRole("Patient");
	    }

	    private void AddRole(string roleName)
	    {
            if (!RoleManager.RoleExists(roleName))
            {
                var role = new IdentityRole { Name = roleName };
                RoleManager.Create(role);
            }
	    }
	}
}