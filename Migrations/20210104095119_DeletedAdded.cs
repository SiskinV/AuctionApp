using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Migrations
{
    public partial class DeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84b5bccf-2ddb-45d3-afcb-4fe35aa7dddb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fa3748f-e3d2-441d-82d8-3775b1c3954c");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "425090de-8eda-4650-ae0c-5620284cbc59", "80622dab-5828-4f2d-86a3-d9b3f4290c5b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7c9af10-cccd-48e0-a2b8-ba102eb3cb05", "2d698116-eefa-45ce-814c-99a62ecc1dd8", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "425090de-8eda-4650-ae0c-5620284cbc59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7c9af10-cccd-48e0-a2b8-ba102eb3cb05");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9fa3748f-e3d2-441d-82d8-3775b1c3954c", "8e0e3c20-fb3d-4235-a7aa-ac1c96aa0009", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84b5bccf-2ddb-45d3-afcb-4fe35aa7dddb", "63f23b47-1535-4bd1-9058-96272dd71b8f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
