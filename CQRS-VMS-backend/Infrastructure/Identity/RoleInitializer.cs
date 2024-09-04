using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = { "Admin", "User", "Manager" };
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }
    }
}
