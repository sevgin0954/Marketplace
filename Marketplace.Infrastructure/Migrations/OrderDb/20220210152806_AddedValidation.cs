using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Infrastructure.Migrations.OrderDb
{
    public partial class AddedValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "61a45093-586f-4e0a-ab25-75ed09177002");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "ae10037d-d29a-4052-9af7-08587b0e8ae3");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "bb619c5d-711d-4143-aea7-02e95039295d");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "f5039077-6cc9-4292-8068-1ee030d10394");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "f11adae9-6bdc-43cd-96e6-d5983af7507a", "Delivered" },
                    { "3ad3b877-a4aa-4c21-8a2f-446f1054e793", "Shipped" },
                    { "567d765f-5662-4f91-86dd-52ce586b12a9", "Processing" },
                    { "299302ba-4bc2-4489-bb15-ec06c43e04b2", "Cancelled" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "299302ba-4bc2-4489-bb15-ec06c43e04b2");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "3ad3b877-a4aa-4c21-8a2f-446f1054e793");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "567d765f-5662-4f91-86dd-52ce586b12a9");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "f11adae9-6bdc-43cd-96e6-d5983af7507a");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "ae10037d-d29a-4052-9af7-08587b0e8ae3", "Delivered" },
                    { "bb619c5d-711d-4143-aea7-02e95039295d", "Shipped" },
                    { "61a45093-586f-4e0a-ab25-75ed09177002", "Processing" },
                    { "f5039077-6cc9-4292-8068-1ee030d10394", "Cancelled" }
                });
        }
    }
}
