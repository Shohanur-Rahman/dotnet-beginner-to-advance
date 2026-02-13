using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinderWebApp.Migrations
{
    /// <inheritdoc />
    public partial class updateTodoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TodoItems",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TodoItems",
                newName: "Details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TodoItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "TodoItems",
                newName: "Address");
        }
    }
}
