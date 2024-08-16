using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class adfasdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 19, 13, 48, 50, 252, DateTimeKind.Local).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 19, 13, 48, 50, 252, DateTimeKind.Local).AddTicks(8430));
        }
    }
}
