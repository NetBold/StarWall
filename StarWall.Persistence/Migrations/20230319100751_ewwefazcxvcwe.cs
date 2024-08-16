using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ewwefazcxvcwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ProfileImage", "RegisterDate" },
                values: new object[] { "Default.png", new DateTime(2023, 3, 19, 13, 37, 51, 826, DateTimeKind.Local).AddTicks(4608) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ProfileImage", "RegisterDate" },
                values: new object[] { "Default", new DateTime(2023, 3, 19, 13, 26, 44, 926, DateTimeKind.Local).AddTicks(3161) });
        }
    }
}
