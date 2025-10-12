using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HWW15.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "Id", "Capacity", "CreatedAt", "PricePerNight", "RoomNumber" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 10, 12, 12, 35, 33, 335, DateTimeKind.Local).AddTicks(8603), 150, "101" },
                    { 2, 4, new DateTime(2025, 10, 12, 12, 35, 33, 335, DateTimeKind.Local).AddTicks(8847), 250, "102" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 12, 12, 35, 33, 324, DateTimeKind.Local).AddTicks(8140), "123", "Admin", "admin" },
                    { 2, new DateTime(2025, 10, 12, 12, 35, 33, 325, DateTimeKind.Local).AddTicks(9609), "123", "Receptionist", "reception" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
