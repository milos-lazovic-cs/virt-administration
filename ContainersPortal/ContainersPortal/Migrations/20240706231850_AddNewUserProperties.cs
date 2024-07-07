using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContainersPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7c594ba-de93-4b7e-b53e-51b97e0cbac2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c248e7-ea8a-4d21-b476-7e8619d4855e");

            migrationBuilder.AddColumn<string>(
                name: "ImageVolumePath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MountVolumePath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bebec11-ac05-4fae-94e0-3f13ba3ab876", null, "Guest", "GUEST" },
                    { "47bbc77b-4344-40b8-8474-c3b7d095b961", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bebec11-ac05-4fae-94e0-3f13ba3ab876");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47bbc77b-4344-40b8-8474-c3b7d095b961");

            migrationBuilder.DropColumn(
                name: "ImageVolumePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MountVolumePath",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e7c594ba-de93-4b7e-b53e-51b97e0cbac2", null, "Guest", "GUEST" },
                    { "e9c248e7-ea8a-4d21-b476-7e8619d4855e", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
