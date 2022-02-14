using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.ShippingOrderDb
{
    public partial class intialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Shipping.Order");

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "Shipping.Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Shipping.Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanceledById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Shipping.Order",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Shipping.Order",
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "5568e295-4fb2-496e-bb93-4ecd6ee1df92", "Delivered" },
                    { "7610d721-3e7b-4d58-9fee-0bb2102734bd", "Shipped" },
                    { "b769292f-1260-4283-b482-b5d5ef3ce806", "Processing" },
                    { "0e6299f8-a320-43bc-bcf3-9213607defcb", "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                schema: "Shipping.Order",
                table: "Orders",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Shipping.Order");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "Shipping.Order");
        }
    }
}
