using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class addin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "4",
                column: "IconPath",
                value: "/StaticFiles/Focused-icon.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "4",
                column: "IconPath",
                value: "/StaticFiles/focused-icon.png");
        }
    }
}
