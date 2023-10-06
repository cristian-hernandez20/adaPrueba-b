using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace adaPrueba_b.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATION6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "Product",
                type: "numeric(15,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "descript", "image", "name", "price", "quantity" },
                values: new object[,]
                {
                    { new Guid("2956ac13-b649-47e6-9ff6-9ae1ed312a3c"), "Estuche para Audifonos Sony MX1000, de muy buena calidad", "", "Estuche audifono", 20000m, 10m },
                    { new Guid("86ca7c32-d03e-41bb-b69a-843522bcc334"), "Audifonos Sony MX1000, de muy buena calidad", "", "Audifonos", 1000000m, 10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: new Guid("2956ac13-b649-47e6-9ff6-9ae1ed312a3c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: new Guid("86ca7c32-d03e-41bb-b69a-843522bcc334"));

            migrationBuilder.DropColumn(
                name: "price",
                table: "Product");
        }
    }
}
