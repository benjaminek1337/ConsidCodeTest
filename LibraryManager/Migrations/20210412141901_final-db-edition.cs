using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManager.Migrations
{
    public partial class finaldbedition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryName",
                value: "Fantasy");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryName",
                value: "Sci-fi");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryName",
                value: "Informatics");

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Type" },
                values: new object[] { "JRR Tolkien", "Book" });

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CategoryId", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[] { null, 2, null, 120, "Star Wars - A New Hope", "DVD" });

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "CategoryId", "IsBorrowable", "Pages", "Title", "Type" },
                values: new object[] { "Gulliksen, Göransson", 3, false, 200, "Användarcentrerad Systemutveckling", "Reference Litterature" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryName",
                value: "Skräck");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryName",
                value: "Komedi");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryName",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Type" },
                values: new object[] { "Tolkien", "Bok" });

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CategoryId", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[] { "Tolkien", 1, 400, null, "Sagan om ringen", "Bok" });

            migrationBuilder.UpdateData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "CategoryId", "IsBorrowable", "Pages", "Title", "Type" },
                values: new object[] { "Tolkien", 1, true, 400, "Sagan om ringen", "Bok" });
        }
    }
}
