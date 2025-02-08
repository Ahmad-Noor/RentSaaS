using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAdvertize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("0db6c4e1-7691-442e-bbc3-7e240d68dccd"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("fba01f32-37b7-4387-aa8a-97318760a3be"), 0, "4021b1f3-1562-4dc9-81b3-fff10af96abf", new DateTime(2025, 2, 6, 12, 33, 50, 334, DateTimeKind.Utc).AddTicks(5739), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AN7Cko8cUBUT2XRb80UvgdqpH0Gag0YH6VWZb00+X/7mczcmxADSE1bcQ5Xp4iv6Uw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("fba01f32-37b7-4387-aa8a-97318760a3be"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("0db6c4e1-7691-442e-bbc3-7e240d68dccd"), 0, "7bdc28a8-8aad-486a-bcd2-ff4dd775c9e8", new DateTime(2025, 2, 6, 10, 14, 56, 404, DateTimeKind.Utc).AddTicks(6422), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AN+B2teB2GdmUklteJp9GONOekZqBY/Vy1ITLv2flb0NN/l1l31cROa7D5Y7zX/N3g==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
