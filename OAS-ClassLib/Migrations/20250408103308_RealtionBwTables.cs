using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAS_ClassLib.Migrations
{
    /// <inheritdoc />
    public partial class RealtionBwTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    AuctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentBid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionId);
                    table.ForeignKey(
                        name: "FK_Auctions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AuctionID",
                table: "Transactions",
                column: "AuctionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BuyerID",
                table: "Transactions",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionID",
                table: "Bids",
                column: "AuctionID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_BuyerID",
                table: "Bids",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ProductId",
                table: "Auctions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionID",
                table: "Bids",
                column: "AuctionID",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_BuyerID",
                table: "Bids",
                column: "BuyerID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Auctions_AuctionID",
                table: "Transactions",
                column: "AuctionID",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_BuyerID",
                table: "Transactions",
                column: "BuyerID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_BuyerID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Auctions_AuctionID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_BuyerID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AuctionID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BuyerID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Bids_AuctionID",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_BuyerID",
                table: "Bids");
        }
    }
}
