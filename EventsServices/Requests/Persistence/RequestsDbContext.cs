using Requests.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Requests.Persistence
{
    public class RequestsDbContext : DbContext
    {
        public DbSet<RequestEvent> RequestEvents { get; set; }

        public RequestsDbContext(DbContextOptions<RequestsDbContext> options)
           : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestEvent>().ToTable("RequestEvent");
        }
    }
}
