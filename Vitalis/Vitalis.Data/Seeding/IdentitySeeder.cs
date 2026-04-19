using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;
using Vitalis.Data.Seeding.Contracts;
using static Vitalis.GCommon.ExceptionMessages;
namespace Vitalis.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] ApplicationRoles = new string[]
        {
            "Admin",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task SeedRolesAsync()
        {
            foreach(string role in ApplicationRoles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);
                    IdentityResult identityRoleResult = await roleManager.CreateAsync(newRole);
                    if (!identityRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException(string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string adminEmail = configuration["UserSeed:AdminUser:Email"] ??
                throw new InvalidOperationException(AdminUserSeedingEmailNotFound);
            string adminPassword = configuration["UserSeed:AdminUser:Password"] ??
                throw new InvalidOperationException(AdminUserSeedingPasswordNotFound);
                
            ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser== null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    JournalEntry = new JournalEntry()
                };

                IdentityResult result = await userManager
                    .CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(AdminUserSeedingException);
                }
            }

            bool isInRole = await userManager
                .IsInRoleAsync(adminUser, ApplicationRoles[0]);

            if (!isInRole)
            {
                IdentityResult result = await userManager
                    .AddToRoleAsync(adminUser, ApplicationRoles[0]);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(AdminUserSeedingException);
                }
            }
        }   
    }
}
