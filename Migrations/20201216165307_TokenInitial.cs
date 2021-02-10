using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionApp.Migrations
{
    public partial class TokenInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e19ce1a-6224-4466-a1a4-7e92e5f99af4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f71f52c-2740-464d-b043-666e7ec50868");

            migrationBuilder.AddColumn<int>(
                name: "tokens",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f1e76a5-9050-4b76-b6a0-1db42cc0e4d6", "c8ac8d1c-7346-439a-ac30-f55be7e460d3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a8e6652-f074-408c-97b6-dcf560937984", "3ecfe652-70a9-4820-8dbc-41a6e25ffbff", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a8e6652-f074-408c-97b6-dcf560937984");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f1e76a5-9050-4b76-b6a0-1db42cc0e4d6");

            migrationBuilder.DropColumn(
                name: "tokens",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e19ce1a-6224-4466-a1a4-7e92e5f99af4", "b463c849-2bab-417c-8b48-ebbbf627a307", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f71f52c-2740-464d-b043-666e7ec50868", "ec59236a-4574-4c9b-ad8f-1081ca11ebaa", "Administrator", "ADMINISTRATOR" });
        }
    }
}
