using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContainersPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59e18f12-2564-492f-8c14-bbe59e4c6dc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6460f399-2eaf-4036-9989-951b1c368c14");

            migrationBuilder.AddColumn<string>(
                name: "OpenSshPrivateKey",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PuttyPrivateKey",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b1e8be32-dae7-48e5-aae0-9d00645d5729", null, "Administrator", "ADMINISTRATOR" },
                    { "fe93a34d-ac74-4abf-a947-ae03360ee40c", null, "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1e8be32-dae7-48e5-aae0-9d00645d5729");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe93a34d-ac74-4abf-a947-ae03360ee40c");

            migrationBuilder.DropColumn(
                name: "OpenSshPrivateKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PuttyPrivateKey",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59e18f12-2564-492f-8c14-bbe59e4c6dc8", null, "Guest", "GUEST" },
                    { "6460f399-2eaf-4036-9989-951b1c368c14", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
