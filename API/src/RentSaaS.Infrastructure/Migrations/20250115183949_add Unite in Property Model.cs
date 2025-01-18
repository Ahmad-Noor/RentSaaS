using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUniteinPropertyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("e1fb52ad-2c17-478d-9d91-33588034e9d0"));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unite",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("74dffcbc-182b-4d42-9dfe-8a38b5fa32ee"), 0, "ab00e80d-a0ba-4dd9-bc7c-2562d9d9bd17", new DateTime(2025, 1, 15, 18, 39, 49, 222, DateTimeKind.Utc).AddTicks(5380), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AAV1LimhBDb4SOcV/GElEo/Gyuk48uzVusC9ysPt7h2DoolEqxRpJW/hhdu5FytI9Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("74dffcbc-182b-4d42-9dfe-8a38b5fa32ee"));

            migrationBuilder.DropColumn(
                name: "Unite",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("e1fb52ad-2c17-478d-9d91-33588034e9d0"), 0, "8f948d3a-fb0e-4774-8597-353759554905", new DateTime(2025, 1, 13, 18, 37, 18, 519, DateTimeKind.Utc).AddTicks(3920), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AEoASkLKHpmCvV344/KrSZ/m4Ddq1XVePfZVMDxP0/1ZQnggDhso/dGmkBJ/phZ8vg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", null });
        }
    }
}
