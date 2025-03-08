using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrganizationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leases_Organizations_OrganizationId",
                table: "leases");

            migrationBuilder.DropIndex(
                name: "IX_leases_OrganizationId",
                table: "leases");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("2dbfe16e-a130-4c3f-83d2-8718e62b800d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "leases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("1708c1af-6002-462f-9ca8-a67b15f4018b"), 0, "60424c94-59ac-457a-90fd-3e39f5f135f0", new DateTime(2025, 3, 8, 11, 20, 29, 840, DateTimeKind.Utc).AddTicks(4738), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AMRCsj/BtGexoteclrjTSwz39W6hXGQFz8czSpzxEuUhZjHyn7tvwOuINKV2htiw1Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("1708c1af-6002-462f-9ca8-a67b15f4018b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "leases",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("2dbfe16e-a130-4c3f-83d2-8718e62b800d"), 0, "1bad715a-7994-4dc5-a874-5c4c72e8d340", new DateTime(2025, 3, 4, 21, 2, 18, 519, DateTimeKind.Utc).AddTicks(5398), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AMJCNQGDhJE2ZZMjqBDUkAQlVQjWW27X1XTwQteKxoGvco2atKbcnXWF2LQnuSVV3Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.CreateIndex(
                name: "IX_leases_OrganizationId",
                table: "leases",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_leases_Organizations_OrganizationId",
                table: "leases",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");
        }
    }
}
