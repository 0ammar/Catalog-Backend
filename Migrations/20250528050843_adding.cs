using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class adding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LookupItems",
                columns: new[] { "Id", "Code", "IconPath", "LookupTypeId", "Name" },
                values: new object[] { "5", "V", "/StaticFiles/star-icon.png", "1", "FavouriteItem" });
        }
    }
}
