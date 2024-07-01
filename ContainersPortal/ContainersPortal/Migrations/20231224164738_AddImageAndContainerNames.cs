using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContainersPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddImageAndContainerNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa7f5c1b-5740-40fd-9521-c00bb1dcd58d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae470fa8-09a0-418b-a420-1ec559f084ec");

            migrationBuilder.AddColumn<string>(
                name: "DockerContainerName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DockerImageName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e7c594ba-de93-4b7e-b53e-51b97e0cbac2", null, "Guest", "GUEST" },
                    { "e9c248e7-ea8a-4d21-b476-7e8619d4855e", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7c594ba-de93-4b7e-b53e-51b97e0cbac2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c248e7-ea8a-4d21-b476-7e8619d4855e");

            migrationBuilder.DropColumn(
                name: "DockerContainerName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DockerImageName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa7f5c1b-5740-40fd-9521-c00bb1dcd58d", null, "Administrator", "ADMINISTRATOR" },
                    { "ae470fa8-09a0-418b-a420-1ec559f084ec", null, "Guest", "GUEST" }
                });
        }
    }
}
