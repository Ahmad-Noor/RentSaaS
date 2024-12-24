using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Identity.Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TenantId" },
                values: new object[] { "997d0ee3-586a-4aa9-8a89-bd8e9af1e1cc", "ADUWMqxGS7Zig4XXWcvHTBofhHuaX1NfmOi0Hq6nU1ZKfOxbEn7iN63DB/aF0hg47g==", "RentSaaS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Identity.Users");

            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9114e2e8-e617-4ff3-b2ef-4f792ecfe05e", "AOMnghLlRymBHoUEd9t+5I5+/TRHtqKioYpm7PMLmSWsPl9qyekZThR4j3cWq0801A==" });
        }
    }
}
