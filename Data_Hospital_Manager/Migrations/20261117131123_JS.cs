using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Hospital_Manager.Migrations
{
    /// <inheritdoc />
    public partial class JS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "BedsCount", "CreatedOn", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Plovdiv, Bulgaria", 300, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "city@hospital.bg", false, "City Hospital", "032123456" },
                    { 2, "Sofia, Bulgaria", 500, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "central@hospital.bg", false, "Central Hospital", "029876543" },
                    { 3, "Stara Zagora, Bulgaria", 250, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "university@hospital.bg", false, "University Hospital", "042123456" }
                });
        }
    }
}
