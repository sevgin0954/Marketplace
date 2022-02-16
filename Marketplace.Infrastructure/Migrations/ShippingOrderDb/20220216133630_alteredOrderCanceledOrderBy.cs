using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.ShippingOrderDb
{
    public partial class alteredOrderCanceledOrderBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "0e6299f8-a320-43bc-bcf3-9213607defcb");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "5568e295-4fb2-496e-bb93-4ecd6ee1df92");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "7610d721-3e7b-4d58-9fee-0bb2102734bd");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "b769292f-1260-4283-b482-b5d5ef3ce806");

            migrationBuilder.RenameColumn(
                name: "CanceledById",
                schema: "Shipping.Order",
                table: "Orders",
                newName: "CanceledOrderBy");

            migrationBuilder.InsertData(
                schema: "Shipping.Order",
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "ce5670ac-a922-4a06-ab9a-3d9b229ff43e", "Delivered" },
                    { "092a783c-11d2-43d4-bd93-6d1e1a237826", "Shipped" },
                    { "690fe63a-df4f-42d1-adbd-d2603523559d", "Processing" },
                    { "a51e6c04-60aa-44dc-828d-d9bba7980357", "Cancelled" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "092a783c-11d2-43d4-bd93-6d1e1a237826");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "690fe63a-df4f-42d1-adbd-d2603523559d");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "a51e6c04-60aa-44dc-828d-d9bba7980357");

            migrationBuilder.DeleteData(
                schema: "Shipping.Order",
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "ce5670ac-a922-4a06-ab9a-3d9b229ff43e");

            migrationBuilder.RenameColumn(
                name: "CanceledOrderBy",
                schema: "Shipping.Order",
                table: "Orders",
                newName: "CanceledById");

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
        }
    }
}
