using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Events.Persistence
{
    [DbContext(typeof(EventDbContext))]
    [Migration("20220411072445_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("Events.Model.Tag", b =>
            {
                b.Property<string>("Value")
                        .HasColumnType("nvarchar(40)");
                //b.Property<string>("EventId")
                //       .HasColumnType("nvarchar(40)");
                b.Property<string>("Id")
                        .HasColumnType("nvarchar(40)");
                b.HasKey("Id");

                b.ToTable("Tags", (string)null);

            });
        }
    }
 }
