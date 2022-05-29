using Providers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Providers.Persistence
{
    public class ProviderDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProviderDb> Providers { get; set; }


        public ProviderDbContext(DbContextOptions<ProviderDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<ProviderDb>().ToTable("Provider");

        }

        public IEnumerable<ProviderDb> GetAll()
        {
            return Providers.Include(provider => provider.Tag);
        }


        public ProviderDb GetProvider(string providerId)
        {
            return Providers.Include(provider => provider.Tag).FirstOrDefault(provider => provider.Id == providerId);
        }

    }

    public class ProviderContextDesignFactory : IDesignTimeDbContextFactory<ProviderDbContext>
    {
        public ProviderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProviderDbContext>()
                .UseSqlServer("Server=DESKTOP-IPP080J;TLS Version=TLS 1.1;Database=providersDb;Trusted_Connection=True;");

            return new ProviderDbContext(optionsBuilder.Options);
        }
    }
}
