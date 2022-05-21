using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Persistence
{
    public class UserDbContextSeed 
    {
        private readonly IPasswordHasher<UserAuth> _passwordHasher = new PasswordHasher<UserAuth>();

        public async Task SeedAsync(UserDbContext context,
            ILogger<UserDbContextSeed> logger, int? retry = 0)
        {
            int retryForAvaiability = retry.Value;
            context.Database.EnsureCreated();

            try
            {

                if (!context.UserAuths.Any())
                {
                    context.UserAuths.AddRange(GetDefaultUser());
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (retryForAvaiability < 10)
                {
                    retryForAvaiability++;

                    logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(UserDbContext));

                    await SeedAsync(context, logger, retryForAvaiability);
                }
            }
        }

        private IEnumerable<UserAuth> GetDefaultUser()
        {
            var user =
            new UserAuth() {
                Id = Guid.NewGuid().ToString(),
                Username = "demouser@microsoft.com",
                Password = "DEMOUSER@MICROSOFT.COM"
            };
            var user1 =
            new UserAuth() {
                Id = Guid.NewGuid().ToString(),
                Username = "demouser@microsoft.com",
                Password = "DEMOUSER@MICROSOFT.COM"
            };

            var user2 =
           new UserAuth() {
               Id = Guid.NewGuid().ToString(),
               Username = "esis2319",
               Password = "1234",
               Type = UserType.Provider
           };

            var user3 =
              new UserAuth() {
                  Id = Guid.NewGuid().ToString(),
                  Username = "esis2318",
                  Password = "1234",
                  Type = UserType.EventHost
              };

            return new List<UserAuth>()
            {
                user,
                user1,
                user2,
                user3
            };
        }
    }
}
