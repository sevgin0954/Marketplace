using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.SalesSellerDb
{
    public partial class intialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sales.Seller");

            migrationBuilder.CreateTable(
                name: "Sellers",
                schema: "Sales.Seller",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "Sales.Seller",
                columns: table => new
                {
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SellerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SellerId2 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => new { x.BuyerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Offers_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "Sales.Seller",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Sellers_SellerId1",
                        column: x => x.SellerId1,
                        principalSchema: "Sales.Seller",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Sellers_SellerId2",
                        column: x => x.SellerId2,
                        principalSchema: "Sales.Seller",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Sales.Seller",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SellerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "Sales.Seller",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerId1",
                        column: x => x.SellerId1,
                        principalSchema: "Sales.Seller",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SellerId",
                schema: "Sales.Seller",
                table: "Offers",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SellerId1",
                schema: "Sales.Seller",
                table: "Offers",
                column: "SellerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SellerId2",
                schema: "Sales.Seller",
                table: "Offers",
                column: "SellerId2");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                schema: "Sales.Seller",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId1",
                schema: "Sales.Seller",
                table: "Products",
                column: "SellerId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers",
                schema: "Sales.Seller");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Sales.Seller");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "Sales.Seller");
        }
    }
}
