using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.SalesProductDb
{
    public partial class intialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sales.Product");

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "Sales.Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Sales.Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalViews = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Sales.Product",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                schema: "Sales.Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Sales.Product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Sales.Product",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "b306a84d-3de2-4a6a-9be5-4870214beadf", "Delivered" },
                    { "c712af99-4097-436a-85cb-abbcf6b9e5fa", "Shipped" },
                    { "f1b74d75-54b6-4949-8070-60333bb62c3e", "Processing" },
                    { "f5d01ad5-ea5d-4dc0-b622-028da940a257", "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProductId",
                schema: "Sales.Product",
                table: "Pictures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StatusId",
                schema: "Sales.Product",
                table: "Products",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures",
                schema: "Sales.Product");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Sales.Product");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "Sales.Product");
        }
    }
}
