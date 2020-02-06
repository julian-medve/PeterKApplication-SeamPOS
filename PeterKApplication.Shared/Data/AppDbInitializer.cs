using Microsoft.AspNetCore.Identity;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Data
{
    public static class AppDbInitializer
    {
        public static void SeedAdmins(UserManager<AppUser> userManager)
        {
            const string admin1Email = "admin@admin";
            if (userManager.FindByEmailAsync(admin1Email).Result == null)
            {
                var user = new AppUser
                {
                    Email = admin1Email,
                    UserName = admin1Email,
                    PhoneNumber = "000000000000",
                    FirstName = "The",
                    LastName = "Admin"
                };

                var result = userManager.CreateAsync(user, "Admin!123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Administrator).Wait();
                }
            }
            
            const string admin2Email = "a@a";
            if (userManager.FindByEmailAsync(admin2Email).Result == null)
            {
                var user = new AppUser
                {
                    Email = admin2Email,
                    UserName = admin2Email,
                    PhoneNumber = "385998877654",
                    FirstName = "An",
                    LastName = "Admin"
                };

                var result = userManager.CreateAsync(user, "Admin!123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Administrator).Wait();
                }
            }
        }

        public static void SeedAgents(UserManager<AppUser> userManager)
        {
            const string agent1Email = "johnnydepp@agent";
            if (userManager.FindByEmailAsync(agent1Email).Result == null)
            {
                var user = new AppUser
                {
                    Email = agent1Email,
                    UserName = agent1Email,
                    PhoneNumber = "00254998877654",
                    FirstName = "Johnny",
                    LastName = "Depp",
                    AgentCode = "QX1"
                };

                var result = userManager.CreateAsync(user, "User123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Agent).Wait();
                }
            }
            
            const string agent2Email = "robertdeniro@agent";
            if (userManager.FindByEmailAsync(agent2Email).Result == null)
            {
                var user = new AppUser
                {
                    Email = agent2Email,
                    UserName = agent2Email,
                    PhoneNumber = "00254998877321",
                    FirstName = "Robert",
                    LastName = "De Niro",
                    AgentCode = "MS1"
                };

                var result = userManager.CreateAsync(user, "User123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Agent).Wait();
                }
            }
            
            const string agent3Email = "cristophwaltz@agent";
            if (userManager.FindByEmailAsync(agent1Email).Result == null)
            {
                var user = new AppUser
                {
                    Email = agent3Email,
                    UserName = agent3Email,
                    PhoneNumber = "00254998877987",
                    FirstName = "Cristoph",
                    LastName = "Waltz"
                };

                var result = userManager.CreateAsync(user, "User123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Agent).Wait();
                }
            }
        }
    }
}