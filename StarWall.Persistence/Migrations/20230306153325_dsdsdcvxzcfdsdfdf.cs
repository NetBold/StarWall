using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dsdsdcvxzcfdsdfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HashedPassword", "RegisterDate" },
                values: new object[] { "19-A5-14-18-9B-61-5A-F5-CE-2F-60-C1-F4-97-46-6C", new DateTime(2023, 3, 6, 19, 3, 25, 309, DateTimeKind.Local).AddTicks(6742) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HashedPassword", "RegisterDate" },
                values: new object[] { "08-B7-DC-6E-8B-36-BC-AA-C1-58-47-82-7B-79-51-A9", new DateTime(2023, 3, 6, 18, 54, 51, 843, DateTimeKind.Local).AddTicks(585) });
        }
    }
}
