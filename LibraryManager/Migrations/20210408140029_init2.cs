using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManager.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_CategoryId",
                table: "LibraryItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Categories_CategoryId",
                table: "LibraryItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Categories_CategoryId",
                table: "LibraryItems");

            migrationBuilder.DropIndex(
                name: "IX_LibraryItems_CategoryId",
                table: "LibraryItems");
        }
    }
}
