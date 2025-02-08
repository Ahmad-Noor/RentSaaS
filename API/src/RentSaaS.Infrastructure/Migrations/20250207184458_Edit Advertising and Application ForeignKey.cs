using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAdvertisingandApplicationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("b15ab41f-2209-43d0-9737-83bcb9f672d3"));

            migrationBuilder.DropColumn(
                name: "Requestbackgroundcheck",
                table: "RentApplications");

            migrationBuilder.DropColumn(
                name: "Requestcreditreport",
                table: "RentApplications");

            migrationBuilder.AddColumn<bool>(
                name: "Requestbackground",
                table: "RentApplications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Requestcredit",
                table: "RentApplications",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("3309f6b9-08cf-494f-83a5-363187986c72"), 0, "5462da70-a320-49bb-b1f6-d4764ffb2d7d", new DateTime(2025, 2, 7, 18, 44, 58, 214, DateTimeKind.Utc).AddTicks(2491), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "ALekzf9EEwvzYrZw7jnX8XdLuSsozMs6DgOSYIMfmV133dxTOt5/VIU89ysXCNO8Pw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("3309f6b9-08cf-494f-83a5-363187986c72"));

            migrationBuilder.DropColumn(
                name: "Requestbackground",
                table: "RentApplications");

            migrationBuilder.DropColumn(
                name: "Requestcredit",
                table: "RentApplications");

            migrationBuilder.AddColumn<bool>(
                name: "Requestbackgroundcheck",
                table: "RentApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Requestcreditreport",
                table: "RentApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("b15ab41f-2209-43d0-9737-83bcb9f672d3"), 0, "00fa109b-e560-4447-80d8-5c22bcb3f17a", new DateTime(2025, 2, 6, 15, 23, 24, 682, DateTimeKind.Utc).AddTicks(3787), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AAm2VxtGX5bbA83TXSwl3KV8XrLPnLfeZ9Gyk2h+73cWGTvhvUigr18btoUEP7mFpg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
