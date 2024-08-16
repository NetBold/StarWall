using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ewwefazcxvcweer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FirstName", "LastName", "RegisterDate", "Username" },
                values: new object[] { "Main", "Main", new DateTime(2023, 3, 19, 13, 48, 50, 252, DateTimeKind.Local).AddTicks(8378), "MainUploader" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveToken", "Email", "FirstName", "HashedPassword", "IsActive", "LastName", "ProfileImage", "RegisterDate", "RoleId", "Username" },
                values: new object[] { 2L, "NullToken", "zolfisahand1386@gmail.com", "Main", "19-A5-14-18-9B-61-5A-F5-CE-2F-60-C1-F4-97-46-6C", true, "Main", "Default.png", new DateTime(2023, 3, 19, 13, 48, 50, 252, DateTimeKind.Local).AddTicks(8430), 3, "MainAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FirstName", "LastName", "RegisterDate", "Username" },
                values: new object[] { "Sahand", "Zolfi", new DateTime(2023, 3, 19, 13, 37, 51, 826, DateTimeKind.Local).AddTicks(4608), "Sahand" });
        }
    }
}
