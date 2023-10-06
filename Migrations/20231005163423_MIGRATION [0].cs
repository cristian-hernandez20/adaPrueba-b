using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adaPrueba_b.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATION0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    addres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    identification = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    nameUser = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    password = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    rol = table.Column<decimal>(type: "numeric(1,0)", nullable: false),
                    token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_nameUser",
                table: "User",
                column: "nameUser",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
