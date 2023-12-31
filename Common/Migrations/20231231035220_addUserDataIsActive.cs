using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Migrations
{
    /// <inheritdoc />
    public partial class addUserDataIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "2d9d8a55-69f6-4a02-ab49-54656c349567", true, "AGybG/hC4bgQUTn+kdm933MrNXQOPuGcdmTkTzWqd3U6qasEAj+FmydAypGNy8zkZg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "3fef0cc0-1f80-4ab0-82f6-88b7835908c2", false, "APQf8sNULv0H9EnKBvH8a3bVjHjdxVpR+lMjEfgN6VEBQSA2U8c9hna8o8TiJwsfLA==" });
        }
    }
}
