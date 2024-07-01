using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContainersPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddIpAddressAndPort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1e8be32-dae7-48e5-aae0-9d00645d5729");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe93a34d-ac74-4abf-a947-ae03360ee40c");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa7f5c1b-5740-40fd-9521-c00bb1dcd58d", null, "Administrator", "ADMINISTRATOR" },
                    { "ae470fa8-09a0-418b-a420-1ec559f084ec", null, "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa7f5c1b-5740-40fd-9521-c00bb1dcd58d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae470fa8-09a0-418b-a420-1ec559f084ec");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b1e8be32-dae7-48e5-aae0-9d00645d5729", null, "Administrator", "ADMINISTRATOR" },
                    { "fe93a34d-ac74-4abf-a947-ae03360ee40c", null, "Guest", "GUEST" }
                });
        }
    }
}
