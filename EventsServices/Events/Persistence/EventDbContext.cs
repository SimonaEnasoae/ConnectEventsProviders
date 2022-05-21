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
        public DbSet<Event> Events { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<Event>().ToTable("Event");


            //builder.Entity<Event>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(p => p.Events)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "EventTag",
            //        j => j
            //            .HasOne<Tag>()
            //            .WithMany()
            //            .HasForeignKey("TagsId")
            //            .HasConstraintName("FK_EventTag_Tags_TagsId")
            //            .OnDelete(DeleteBehavior.Cascade),
            //        j => j
            //            .HasOne<Event>()
            //            .WithMany()
            //            .HasForeignKey("EventsId")
            //            .HasConstraintName("FK_EventTag_Events_EventsId")
            //            .OnDelete(DeleteBehavior.ClientCascade));

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

        public IEnumerable<Event> GetAllByOrganiser(string organiserId)
        {
            return Events.Where(ev => ev.OrganiserId == organiserId).Include(order => order.EventTags)
                              .ThenInclude(orderProducts => orderProducts.Tag);
        }

        public Event GetEventById(string id)
        {
            return Events.Where(ev => ev.Id == id).Include(order => order.EventTags).
                ThenInclude(orderProducts => orderProducts.Tag).FirstOrDefault();
        }

        public IEnumerable<Event> GetAll()
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
