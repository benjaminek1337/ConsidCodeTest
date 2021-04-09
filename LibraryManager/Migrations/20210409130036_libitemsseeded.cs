using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManager.Migrations
{
    public partial class libitemsseeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Borrower",
                table: "LibraryItems",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.InsertData(
                table: "LibraryItems",
                columns: new[] { "Id", "Author", "BorrowDate", "Borrower", "CategoryId", "IsBorrowable", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[] { 1, "Tolkien", null, null, 1, true, 400, null, "Sagan om ringen", "Bok" });

            migrationBuilder.InsertData(
                table: "LibraryItems",
                columns: new[] { "Id", "Author", "BorrowDate", "Borrower", "CategoryId", "IsBorrowable", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[] { 2, "Tolkien", null, null, 1, true, 400, null, "Sagan om ringen", "Bok" });

            migrationBuilder.InsertData(
                table: "LibraryItems",
                columns: new[] { "Id", "Author", "BorrowDate", "Borrower", "CategoryId", "IsBorrowable", "Pages", "RunTimeMinutes", "Title", "Type" },
                values: new object[] { 3, "Tolkien", null, null, 1, true, 400, null, "Sagan om ringen", "Bok" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibraryItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Borrower",
                table: "LibraryItems",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }
    }
}
