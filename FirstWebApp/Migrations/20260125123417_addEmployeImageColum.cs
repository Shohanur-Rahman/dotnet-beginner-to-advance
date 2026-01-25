using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeImageColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Employee");
        }
    }
}
