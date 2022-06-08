using Events.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistence
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventDb>
    {
        public void Configure(EntityTypeBuilder<EventDb> builder)
        {

        }
    }
}
