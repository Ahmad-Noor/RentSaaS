using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAvertisingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("e4577c96-1c9f-4ff1-853a-305a7c617618"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Organizations",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Advertising",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Leads = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertising", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertising_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("ee187363-a81b-47e3-9633-2eed4e3221f0"), 0, "f955a668-967c-4fe3-85b4-6fa3f1ae0118", new DateTime(2025, 2, 6, 8, 59, 57, 435, DateTimeKind.Utc).AddTicks(8242), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "ABOuXoBalZCIPUyNxaB6opgCSdTIqaoywB1aGvvX8Y+030gce4jDcxRngNc7KT3bdw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "IsDeleted",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Advertising_PropertyId",
                table: "Advertising",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertising");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("ee187363-a81b-47e3-9633-2eed4e3221f0"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Organizations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("e4577c96-1c9f-4ff1-853a-305a7c617618"), 0, "09d9b941-b049-4815-9cfc-77a43821dc55", new DateTime(2025, 2, 2, 19, 32, 7, 757, DateTimeKind.Utc).AddTicks(6588), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "ALZSMuOWKqJSE2BZwS0VxBGvNzO2GpdRrjMqinBsJvdN+Sytn+AE+1K640mgnHB4Dw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "IsDeleted",
                value: false);
        }
    }
}
