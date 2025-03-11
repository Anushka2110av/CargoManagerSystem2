using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace CargoManagerSystem.Models
{
    public class EmployeeDetails
    {
     public void CreateRolesAndAdminUser(string email, string password, string Role)
        {
            using (var context = new ApplicationContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Ensure "Admin" role exists
                if (!roleManager.RoleExists(Role))
                {
                    roleManager.Create(new IdentityRole(Role));
                }


                // Create admin user only if not exists
                var UserEmail = email;
                var adminUser = userManager.FindByEmail(UserEmail);

                if (adminUser == null)
                {
                    var newAdmin = new ApplicationUser { UserName = UserEmail, Email = UserEmail };
                    var result = userManager.Create(newAdmin, password); // Set default password

                    if (result.Succeeded)
                    {
                        userManager.AddToRole(newAdmin.Id, Role);
                    }
                }
            }
        }
    }
}