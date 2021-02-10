using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Migrations
{
    public partial class AuctionInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "096566eb-652d-4652-ae1a-65ccb6ff0b23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa33cb1e-d317-4daf-ae6e-54bd743f3d0c");

            migrationBuilder.CreateTable(
                name: "auctions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    dateCreate = table.Column<DateTime>(nullable: false),
                    dateOpen = table.Column<DateTime>(nullable: false),
                    dateExpire = table.Column<DateTime>(nullable: false),
                    startPrice = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    bidAmount = table.Column<int>(nullable: false),
                    state = table.Column<string>(nullable: false),
                    userId = table.Column<string>(nullable: false),
                    rowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    lastBidder = table.Column<string>(nullable: true),
                    image = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auctions", x => x.id);
                    table.ForeignKey(
                        name: "FK_auctions_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9fa3748f-e3d2-441d-82d8-3775b1c3954c", "8e0e3c20-fb3d-4235-a7aa-ac1c96aa0009", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84b5bccf-2ddb-45d3-afcb-4fe35aa7dddb", "63f23b47-1535-4bd1-9058-96272dd71b8f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_auctions_userId",
                table: "auctions",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auctions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84b5bccf-2ddb-45d3-afcb-4fe35aa7dddb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fa3748f-e3d2-441d-82d8-3775b1c3954c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa33cb1e-d317-4daf-ae6e-54bd743f3d0c", "a765869e-54b9-482e-8d60-5d7bba0e73ad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "096566eb-652d-4652-ae1a-65ccb6ff0b23", "2889adeb-8f00-447d-ba94-954197c7f348", "Administrator", "ADMINISTRATOR" });
        }
    }
}
