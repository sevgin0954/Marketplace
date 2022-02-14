using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.SalesBuyerDb
{
    public partial class intialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sales.Buyer");

            migrationBuilder.CreateTable(
                name: "Buyers",
                schema: "Sales.Buyer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                schema: "Sales.Buyer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BuyerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "Sales.Buyer",
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offer_Buyers_BuyerId1",
                        column: x => x.BuyerId1,
                        principalSchema: "Sales.Buyer",
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_BuyerId",
                schema: "Sales.Buyer",
                table: "Offer",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_BuyerId1",
                schema: "Sales.Buyer",
                table: "Offer",
                column: "BuyerId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer",
                schema: "Sales.Buyer");

            migrationBuilder.DropTable(
                name: "Buyers",
                schema: "Sales.Buyer");
        }
    }
}
