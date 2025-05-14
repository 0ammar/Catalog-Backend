using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Sync_StatusId_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Admin"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CH_Name_Length", "LEN(Username) >= 3");
                });

            migrationBuilder.CreateTable(
                name: "SubOnes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOnes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubOnes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LookupItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LookupTypeId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupItems_LookupTypes_LookupTypeId",
                        column: x => x.LookupTypeId,
                        principalTable: "LookupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTwos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubOneId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTwos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTwos_Groups_SubOneId",
                        column: x => x.SubOneId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubTwos_SubOnes_SubOneId",
                        column: x => x.SubOneId,
                        principalTable: "SubOnes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubThrees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubOneId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    SubTwoId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubThrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubThrees_Groups_SubOneId",
                        column: x => x.SubOneId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubThrees_SubOnes_SubOneId",
                        column: x => x.SubOneId,
                        principalTable: "SubOnes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubThrees_SubTwos_SubTwoId",
                        column: x => x.SubTwoId,
                        principalTable: "SubTwos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<string>(type: "nvarchar(5)", nullable: false, defaultValue: "2"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GroupId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    SubOneId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    SubTwoId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    SubThreeId = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemNo);
                    table.ForeignKey(
                        name: "FK_Items_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_LookupItems_StatusId",
                        column: x => x.StatusId,
                        principalTable: "LookupItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SubOnes_SubOneId",
                        column: x => x.SubOneId,
                        principalTable: "SubOnes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SubThrees_SubThreeId",
                        column: x => x.SubThreeId,
                        principalTable: "SubThrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SubTwos_SubTwoId",
                        column: x => x.SubTwoId,
                        principalTable: "SubTwos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "1", "ItemStatus" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { "1", "$2a$11$BhE4mnYVxUVDHHQpAiLeWeivEziH9/M9DF.UnGFZqp7WMr2ag7ki2", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "LookupItems",
                columns: new[] { "Id", "Code", "IconPath", "LookupTypeId", "Name" },
                values: new object[,]
                {
                    { "1", "I", "/StaticFiles/inactive-icon.png", "1", "InactiveItem" },
                    { "2", "A", "/StaticFiles/active-icon.png", "1", "ActiveItem" },
                    { "3", "N", "/StaticFiles/new-icon.png", "1", "NewItem" },
                    { "4", "F", "/StaticFiles/focused-icon.png", "1", "FocusedItem" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Items_GroupId",
                table: "Items",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemNo",
                table: "Items",
                column: "ItemNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StatusId",
                table: "Items",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubOneId",
                table: "Items",
                column: "SubOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubThreeId",
                table: "Items",
                column: "SubThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubTwoId",
                table: "Items",
                column: "SubTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_LookupTypeId",
                table: "LookupItems",
                column: "LookupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOnes_GroupId",
                table: "SubOnes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubOnes_Name",
                table: "SubOnes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SubThrees_Name",
                table: "SubThrees",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SubThrees_SubOneId",
                table: "SubThrees",
                column: "SubOneId");

            migrationBuilder.CreateIndex(
                name: "IX_SubThrees_SubTwoId",
                table: "SubThrees",
                column: "SubTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTwos_Name",
                table: "SubTwos",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SubTwos_SubOneId",
                table: "SubTwos",
                column: "SubOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LookupItems");

            migrationBuilder.DropTable(
                name: "SubThrees");

            migrationBuilder.DropTable(
                name: "LookupTypes");

            migrationBuilder.DropTable(
                name: "SubTwos");

            migrationBuilder.DropTable(
                name: "SubOnes");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
