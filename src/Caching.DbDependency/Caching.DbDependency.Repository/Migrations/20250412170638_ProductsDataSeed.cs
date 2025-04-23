using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Caching.Demo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ProductsDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3db7e087-bec7-4605-8c73-89edd88eb693"), "Product 3", 30.0m },
                    { new Guid("a42e98cc-ae38-4fa9-abb9-350ccf654f93"), "Product 2", 20.0m },
                    { new Guid("ed1dd774-14a8-45cd-99bb-5567dba9aad7"), "Product 1", 10.0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3db7e087-bec7-4605-8c73-89edd88eb693"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a42e98cc-ae38-4fa9-abb9-350ccf654f93"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed1dd774-14a8-45cd-99bb-5567dba9aad7"));
        }
    }
}
