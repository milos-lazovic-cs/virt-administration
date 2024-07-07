using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContainersPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bebec11-ac05-4fae-94e0-3f13ba3ab876");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47bbc77b-4344-40b8-8474-c3b7d095b961");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bdda0ae-950c-4979-9560-ada6947f3566", null, "User", "USER" },
                    { "ced941c9-18bd-4c80-8e3a-7ebe25fa1cde", null, "Administrator", "ADMINISTRATOR" },
                    { "f9af1907-4016-4b3d-a4d0-c6d9de8fa807", null, "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bdda0ae-950c-4979-9560-ada6947f3566");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ced941c9-18bd-4c80-8e3a-7ebe25fa1cde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9af1907-4016-4b3d-a4d0-c6d9de8fa807");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bebec11-ac05-4fae-94e0-3f13ba3ab876", null, "Guest", "GUEST" },
                    { "47bbc77b-4344-40b8-8474-c3b7d095b961", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { new Guid("398d10fe-4b8d-4606-8e9c-bd2c78d4e001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anna Simmons" },
                    { new Guid("e310a6cb-6677-4aa6-93c7-2763956f7a97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark Miens" }
                });
        }
    }
}
