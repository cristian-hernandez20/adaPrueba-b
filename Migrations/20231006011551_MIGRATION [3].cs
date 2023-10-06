using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adaPrueba_b.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATION3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
