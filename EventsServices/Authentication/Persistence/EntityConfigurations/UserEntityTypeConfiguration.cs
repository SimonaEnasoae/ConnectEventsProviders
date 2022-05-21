using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Persistence.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserAuth>
    {
        public void Configure(EntityTypeBuilder<UserAuth> builder)
        {
            //builder.ToTable("Users");

            //builder.HasKey(user => user.Id);

            //builder.Property(user => user.Id)
            //    .UseHiLo("user_id")
            //    .IsRequired();

            //builder.Property(user => user.Username)
            //    .IsRequired()
            //    .HasMaxLength(100);

            //builder.Property(user => user.Password)
            //   .IsRequired()
            //   .HasMaxLength(100);
        }
    }

}
