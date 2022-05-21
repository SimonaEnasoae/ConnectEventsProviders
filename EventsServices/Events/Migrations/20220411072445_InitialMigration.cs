using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Persistence
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Tags",
                 columns: table => new {
                     Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                     Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                     //EventId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),

                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_tags", x => x.Id);
                 });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
                //name: "Tags");
        }
    }
}
