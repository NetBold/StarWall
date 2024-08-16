using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fasdfasdfwrg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 15, 22, 48, 26, 997, DateTimeKind.Local).AddTicks(849));

            migrationBuilder.InsertData(
                table: "WebSiteInfos",
                columns: new[] { "Id", "Address", "Email", "Instagram", "LongDescription", "PhoneNumber1", "PhoneNumber2", "PoweredBy", "ShortDescription", "Telegram", "Title", "Twitter" },
                values: new object[] { 1, "Iran - Tabriz - Valiasr Street", "starsite.iran@gmail.com", "StarWall_inst", "Download amazing wallpapers from starwall and set it as your backgroun image on your devices.", "09031813679", "09031813679", "NetBold", "Wallpapers like Stars", "StarWall_Tel", "StarWall", "StarWall_Twi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WebSiteInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 15, 22, 39, 55, 360, DateTimeKind.Local).AddTicks(902));
        }
    }
}
