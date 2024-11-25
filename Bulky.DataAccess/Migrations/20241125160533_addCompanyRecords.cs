using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "San Francisco", "Tech Corp", "555-1234", "94103", "CA", "123 Tech Avenue" },
                    { 2, "Austin", "Innovate LLC", "555-5678", "73301", "TX", "456 Innovation Drive" },
                    { 3, "Seattle", "Bright Futures Inc.", "555-7890", "98101", "WA", "789 Future Blvd" },
                    { 4, "Denver", "Pioneer Tech", "555-1010", "80202", "CO", "101 Pioneer Way" },
                    { 5, "Portland", "Eco Innovators", "555-2020", "97209", "OR", "202 Green Lane" },
                    { 6, "Chicago", "NextGen Solutions", "555-3030", "60601", "IL", "303 Progress Rd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
