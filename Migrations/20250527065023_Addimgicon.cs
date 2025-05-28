using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Addimgicon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "5",
                column: "IconPath",
                value: "/StaticFiles/star-icon.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "5",
                column: "IconPath",
                value: "/StaticFiles/focused-icon.png");
        }
    }
}
