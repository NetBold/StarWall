using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class zsdfsdg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 15, 22, 39, 55, 360, DateTimeKind.Local).AddTicks(902));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 6, 19, 3, 25, 309, DateTimeKind.Local).AddTicks(6742));
        }
    }
}
