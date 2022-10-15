using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Persistence.Migrations.SalesDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PendingOffersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerEntitySellerEntity",
                columns: table => new
                {
                    BannedBuyersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellersWhereBuyerIsBannedId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerEntitySellerEntity", x => new { x.BannedBuyersId, x.SellersWhereBuyerIsBannedId });
                    table.ForeignKey(
                        name: "FK_BuyerEntitySellerEntity_Buyers_BannedBuyersId",
                        column: x => x.BannedBuyersId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerEntitySellerEntity_Sellers_SellersWhereBuyerIsBannedId",
                        column: x => x.SellersWhereBuyerIsBannedId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferEntity_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferEntity_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuyerEntityOfferEntity",
                columns: table => new
                {
                    BuyersWithStartedOffersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartedPendingOffersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerEntityOfferEntity", x => new { x.BuyersWithStartedOffersId, x.StartedPendingOffersId });
                    table.ForeignKey(
                        name: "FK_BuyerEntityOfferEntity_Buyers_BuyersWithStartedOffersId",
                        column: x => x.BuyersWithStartedOffersId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerEntityOfferEntity_OfferEntity_StartedPendingOffersId",
                        column: x => x.StartedPendingOffersId,
                        principalTable: "OfferEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "PendingOffersCount" },
                values: new object[] { "1", 1 });

            migrationBuilder.InsertData(
                table: "Sellers",
                column: "Id",
                value: "1");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "PriceCurrency", "SellerId", "Status" },
                values: new object[] { "1", 1m, "BGN", "1", "IN Sale" });

            migrationBuilder.InsertData(
                table: "OfferEntity",
                columns: new[] { "Id", "BuyerId", "Message", "ProductId", "RejectMessage", "SellerId", "Status" },
                values: new object[] { "1", "1", "message", "1", "Reject message", "1", "Pending" });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerEntityOfferEntity_StartedPendingOffersId",
                table: "BuyerEntityOfferEntity",
                column: "StartedPendingOffersId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerEntitySellerEntity_SellersWhereBuyerIsBannedId",
                table: "BuyerEntitySellerEntity",
                column: "SellersWhereBuyerIsBannedId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEntity_BuyerId",
                table: "OfferEntity",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEntity_ProductId",
                table: "OfferEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEntity_SellerId",
                table: "OfferEntity",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerEntityOfferEntity");

            migrationBuilder.DropTable(
                name: "BuyerEntitySellerEntity");

            migrationBuilder.DropTable(
                name: "OfferEntity");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
