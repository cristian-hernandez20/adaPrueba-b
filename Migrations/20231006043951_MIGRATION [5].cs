using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adaPrueba_b.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATION5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    descript = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shopping",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    id_product = table.Column<Guid>(type: "uuid", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopping", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shopping_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shopping_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_id_product",
                table: "Shopping",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_id_user",
                table: "Shopping",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shopping");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "id");
        }
    }
}
