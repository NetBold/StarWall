using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dasgdfgdffdvbcvb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 26, 18, 16, 18, 4, DateTimeKind.Local).AddTicks(9505));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 26, 18, 16, 18, 5, DateTimeKind.Local).AddTicks(158));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 24, 3, 10, 37, 621, DateTimeKind.Local).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 24, 3, 10, 37, 621, DateTimeKind.Local).AddTicks(5493));
        }
    }
}
