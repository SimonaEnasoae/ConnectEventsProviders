using Events.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistence
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //builder.Property(ci => ci.EndDate)
            //.UseIdentityColumn("end_date");

            //builder.HasMany(ci => ci.Tags)
            //    .WithMany(t => t.Events)
            //    .UsingEntity<EventTag>(
            //    j => j
            //        .HasOne(pt => pt.Tag)
            //        .WithMany(t => t.EVE)
            //        .HasForeignKey(pt => pt.TagId),
            //    j => j
            //        .HasOne(pt => pt.Event)
            //        .WithMany(p => p.PostTags)
            //        .HasForeignKey(pt => pt.PostId),
            //    j =>
            //    {
            //        j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            //        j.HasKey(t => new { t.PostId, t.TagId });
            //    });
            //builder.HasMany<Tag>(o => o.Tags).WithOne("EventId");


        }
    }
}
