using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterecordpaymentfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenseFiles_RecordPayments_RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.DropIndex(
                name: "IX_expenseFiles_RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("ea9feaac-8cb5-4fcb-b390-ef92d5c5bcb2"));

            migrationBuilder.DropColumn(
                name: "RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("78754f9b-3fcb-4774-b2cf-132e6e1824b3"), 0, "a4865cb9-3e3e-4d08-a016-32de165f475a", new DateTime(2025, 3, 2, 22, 43, 54, 828, DateTimeKind.Utc).AddTicks(8267), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AC2SpVlWIkYAr6Og+eVu9g71sUWIb22zgEqGd+iusMR/HLVFmlWpitz65RxKkEpa2Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("78754f9b-3fcb-4774-b2cf-132e6e1824b3"));

            migrationBuilder.AddColumn<Guid>(
                name: "RecordPaymentId",
                table: "expenseFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("ea9feaac-8cb5-4fcb-b390-ef92d5c5bcb2"), 0, "d46b51d8-1ef7-4a76-a186-6dbc8fecc8a8", new DateTime(2025, 3, 2, 22, 32, 11, 467, DateTimeKind.Utc).AddTicks(929), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AK9crL5niEjGcptGtUzPra319vSOhoONf5UEXZAaUe5LJ8fVAWdDreByzuuuhTdmbg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.CreateIndex(
                name: "IX_expenseFiles_RecordPaymentId",
                table: "expenseFiles",
                column: "RecordPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_expenseFiles_RecordPayments_RecordPaymentId",
                table: "expenseFiles",
                column: "RecordPaymentId",
                principalTable: "RecordPayments",
                principalColumn: "Id");
        }
    }
}
