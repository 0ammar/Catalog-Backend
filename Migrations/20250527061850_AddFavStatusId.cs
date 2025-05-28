using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFavStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LookupItems",
                columns: new[] { "Id", "Code", "IconPath", "LookupTypeId", "Name" },
                values: new object[] { "5", "V", "/StaticFiles/focused-icon.png", "1", "FavouriteItem" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LookupItems",
                keyColumn: "Id",
                keyValue: "5");
        }
    }
}
