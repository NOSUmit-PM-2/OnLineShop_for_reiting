using Microsoft.AspNetCore.Identity;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
        {
            var adminEmail = "admin@nosu.ru";
            var adminPassword = "_Aa123456";

            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
            { 
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
            }

            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
            }
            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail, Address = "AdminAddress", Namber = "AdminNamber" };
                var result = userManager.CreateAsync(admin, "_Aa123456").Result;
                if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
                    }
            }
        }
    }
}
