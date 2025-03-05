using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("cfe4b5e1-f922-4a74-8d29-e9b739810d17"));

            migrationBuilder.RenameColumn(
                name: "PhotoSize",
                table: "MaintenancePhoto",
                newName: "FileSize");

            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "MaintenancePhoto",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Maintenance",
                newName: "File");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("2dbfe16e-a130-4c3f-83d2-8718e62b800d"), 0, "1bad715a-7994-4dc5-a874-5c4c72e8d340", new DateTime(2025, 3, 4, 21, 2, 18, 519, DateTimeKind.Utc).AddTicks(5398), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AMJCNQGDhJE2ZZMjqBDUkAQlVQjWW27X1XTwQteKxoGvco2atKbcnXWF2LQnuSVV3Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("2dbfe16e-a130-4c3f-83d2-8718e62b800d"));

            migrationBuilder.RenameColumn(
                name: "FileSize",
                table: "MaintenancePhoto",
                newName: "PhotoSize");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "MaintenancePhoto",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "Maintenance",
                newName: "Photo");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("cfe4b5e1-f922-4a74-8d29-e9b739810d17"), 0, "6c60a6e1-098b-4ea7-8776-da63ba6620cd", new DateTime(2025, 3, 4, 19, 38, 52, 719, DateTimeKind.Utc).AddTicks(7377), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AG0RPE46G0yV3Tz+xAV8TG7L8Myx7kyLngHvHLgVQV3ynlh92zHL0eN/ilGwrBdCMQ==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
