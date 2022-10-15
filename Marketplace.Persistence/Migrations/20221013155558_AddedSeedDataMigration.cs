using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Persistence.Migrations
{
    public partial class AddedSeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "1", "Description", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
