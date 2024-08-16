using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ewwefazcxvc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 19, 13, 26, 44, 926, DateTimeKind.Local).AddTicks(3161));

            migrationBuilder.InsertData(
                table: "WallpaperGenreCategories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 3L, "Games" },
                    { 4L, "Anime" },
                    { 5L, "Minimalist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WallpaperGenreCategories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "WallpaperGenreCategories",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "WallpaperGenreCategories",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 15, 22, 48, 26, 997, DateTimeKind.Local).AddTicks(849));
        }
    }
}
