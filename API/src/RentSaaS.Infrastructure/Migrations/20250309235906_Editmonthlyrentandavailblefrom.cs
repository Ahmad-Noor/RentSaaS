using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Editmonthlyrentandavailblefrom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("17c8676d-f193-457d-8a5c-8f0a80b2c564"));

            migrationBuilder.RenameColumn(
                name: "MontholyRent",
                table: "Advertising",
                newName: "MonthlyRent");

            migrationBuilder.RenameColumn(
                name: "AvailableForm",
                table: "Advertising",
                newName: "availableFrom");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("28cb6448-856e-48c8-915a-09ed8d9c8c44"), 0, "e6a52f4b-36e7-43f3-a7ee-c75b54ec5111", new DateTime(2025, 3, 9, 23, 59, 4, 436, DateTimeKind.Utc).AddTicks(5369), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AC0t7Uf7A9vDOTIF56CIlVJ7y+ZuLOUefKtvz9ItHfcbxJIR8G6WRG4Mdp6nJ5Xdpw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("28cb6448-856e-48c8-915a-09ed8d9c8c44"));

            migrationBuilder.RenameColumn(
                name: "availableFrom",
                table: "Advertising",
                newName: "AvailableForm");

            migrationBuilder.RenameColumn(
                name: "MonthlyRent",
                table: "Advertising",
                newName: "MontholyRent");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("17c8676d-f193-457d-8a5c-8f0a80b2c564"), 0, "d731187b-9c14-4942-83a8-4f77f08091fb", new DateTime(2025, 3, 9, 22, 6, 17, 193, DateTimeKind.Utc).AddTicks(4587), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "ACOyUbhn/LqieK8WhKw4AOxhYBoZEpFeODlbVr9e8lXpNoTFJI0fxWEtOp+8Orz30g==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
