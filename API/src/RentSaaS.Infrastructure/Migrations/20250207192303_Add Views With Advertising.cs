using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddViewsWithAdvertising : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("3309f6b9-08cf-494f-83a5-363187986c72"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("8c409497-bcc4-4b20-8ddc-ff8b80493445"), 0, "51fe709a-84a3-4036-a751-415b70c04e16", new DateTime(2025, 2, 7, 19, 23, 3, 160, DateTimeKind.Utc).AddTicks(4627), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AKPY4hf+VeajTyzf4IsIJUeMu2RBISNPr3VxlxwZFua0h7NpllwpHUIgOk473KpWTQ==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("8c409497-bcc4-4b20-8ddc-ff8b80493445"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("3309f6b9-08cf-494f-83a5-363187986c72"), 0, "5462da70-a320-49bb-b1f6-d4764ffb2d7d", new DateTime(2025, 2, 7, 18, 44, 58, 214, DateTimeKind.Utc).AddTicks(2491), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "ALekzf9EEwvzYrZw7jnX8XdLuSsozMs6DgOSYIMfmV133dxTOt5/VIU89ysXCNO8Pw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
