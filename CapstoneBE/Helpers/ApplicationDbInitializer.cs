using CapstoneBE.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CapstoneBE.Utils.APIConstants;

namespace CapstoneBE.Helpers
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<CapstoneBEUser> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                CapstoneBEUser user = new CapstoneBEUser
                {
                    Address = "FPT HCM University",
                    Email = "kenbiboykute@gmail.com",
                    UserName = "Admin",
                    IsDel = false,
                    Name = "Tuan Anh Hoang",
                    PhoneNumber = "0824338079",
                    Role = "Administrator"
                };
                IdentityResult createResult = userManager.CreateAsync(user, "Administrator0").Result;
                if (createResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.AdminRole).Wait();
                }
            }
        }
    }
}