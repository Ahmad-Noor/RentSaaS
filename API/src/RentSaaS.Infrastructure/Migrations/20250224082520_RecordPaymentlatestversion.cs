using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecordPaymentlatestversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordPaymentFiles_RecordPayments_RecordPaymentId",
                table: "RecordPaymentFiles");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("7cfd833e-dce3-4662-a565-f0334b92ef71"));

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "RecordPaymentFiles");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptsFiles",
                table: "RecordPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RecordPaymentId",
                table: "RecordPaymentFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecordPaymentId",
                table: "expenseFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("f59f8e10-33d4-451e-ab73-e14635f612aa"), 0, "e9a59e68-2e04-477b-bb37-8eb8ed66f120", new DateTime(2025, 2, 24, 8, 25, 18, 179, DateTimeKind.Utc).AddTicks(7529), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AJK6sl4rF2GffW3OPOFyZ4NMFz4bT+hoiX8wpl/Lkf2hseZ0LzVdLBt6uCdeObGlhg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecordPaymentFiles_RecordPayments_RecordPaymentId",
                table: "RecordPaymentFiles",
                column: "RecordPaymentId",
                principalTable: "RecordPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenseFiles_RecordPayments_RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordPaymentFiles_RecordPayments_RecordPaymentId",
                table: "RecordPaymentFiles");

            migrationBuilder.DropIndex(
                name: "IX_expenseFiles_RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("f59f8e10-33d4-451e-ab73-e14635f612aa"));

            migrationBuilder.DropColumn(
                name: "ReceiptsFiles",
                table: "RecordPayments");

            migrationBuilder.DropColumn(
                name: "RecordPaymentId",
                table: "expenseFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecordPaymentId",
                table: "RecordPaymentFiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "RecordPaymentFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("7cfd833e-dce3-4662-a565-f0334b92ef71"), 0, "c5fd040d-0748-4dca-8864-6de0a328ab9a", new DateTime(2025, 2, 23, 21, 2, 7, 1, DateTimeKind.Utc).AddTicks(3527), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AN+aA0ze5xnhQnhOLQiIIZv+ulVkx8SypqpBs44oQsoQsQL9V+Az3qVsovLxH40TrA==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecordPaymentFiles_RecordPayments_RecordPaymentId",
                table: "RecordPaymentFiles",
                column: "RecordPaymentId",
                principalTable: "RecordPayments",
                principalColumn: "Id");
        }
    }
}
