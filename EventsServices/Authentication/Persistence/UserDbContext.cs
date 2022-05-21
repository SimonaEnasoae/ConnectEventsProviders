using Authentication.Models;
using Authentication.Persistence;
using Authentication.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authentication.Persistence
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserAuth> UserAuths { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }



    public class UserContextDesignFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer("Server=DESKTOP-IPP080J;TLS Version=TLS 1.1;Database=eventsDb;Trusted_Connection=True;");

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}