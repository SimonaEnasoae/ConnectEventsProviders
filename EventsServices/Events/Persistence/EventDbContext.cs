using Events.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Events.Persistence
{
    public class EventDbContext : DbContext
    {
        public DbSet<EventDb> Events { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<EventDb>().ToTable("Event");

            builder.Entity<EventTag>()
                 .HasKey(t => new { t.EventId, t.TagId });

            builder.Entity<EventTag>().HasOne(m => m.Event)
            .WithMany(m => m.EventTags)
            .HasForeignKey(m => m.EventId);

            builder.Entity<EventTag>().HasOne(m => m.Tag)
            .WithMany(m => m.EventTags)
            .HasForeignKey(m => m.TagId);

            builder.ApplyConfiguration(new EventTypeConfiguration());

        }

        public IEnumerable<EventDb> GetAllByOrganiser(string organiserId)
        {
            return Events.Where(ev => ev.OrganiserId == organiserId).Include(order => order.EventTags)
                              .ThenInclude(orderProducts => orderProducts.Tag);
        }

        public EventDb GetEventById(string id)
        {
            return Events.Where(ev => ev.Id == id).Include(order => order.EventTags).
                ThenInclude(orderProducts => orderProducts.Tag).FirstOrDefault();
        }

        public IEnumerable<EventDb> GetAll()
        {
            return Events.Include(order => order.EventTags)
                              .ThenInclude(orderProducts => orderProducts.Tag);
        }
    }

    public class UserContextDesignFactory : IDesignTimeDbContextFactory<EventDbContext>
    {
        public EventDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventDbContext>()
                .UseSqlServer("Server=DESKTOP-IPP080J;TLS Version=TLS 1.1;Database=eventsDb;Trusted_Connection=True;");

            return new EventDbContext(optionsBuilder.Options);
        }
    }
}
