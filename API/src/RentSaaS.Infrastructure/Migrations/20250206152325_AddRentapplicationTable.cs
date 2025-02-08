using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRentapplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("fba01f32-37b7-4387-aa8a-97318760a3be"));

            migrationBuilder.CreateTable(
                name: "RentApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requestbackgroundcheck = table.Column<bool>(type: "bit", nullable: false),
                    Requestcreditreport = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_RentApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentApplications_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("b15ab41f-2209-43d0-9737-83bcb9f672d3"), 0, "00fa109b-e560-4447-80d8-5c22bcb3f17a", new DateTime(2025, 2, 6, 15, 23, 24, 682, DateTimeKind.Utc).AddTicks(3787), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AAm2VxtGX5bbA83TXSwl3KV8XrLPnLfeZ9Gyk2h+73cWGTvhvUigr18btoUEP7mFpg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.CreateIndex(
                name: "IX_RentApplications_PropertyId",
                table: "RentApplications",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentApplications");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("b15ab41f-2209-43d0-9737-83bcb9f672d3"));

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("fba01f32-37b7-4387-aa8a-97318760a3be"), 0, "4021b1f3-1562-4dc9-81b3-fff10af96abf", new DateTime(2025, 2, 6, 12, 33, 50, 334, DateTimeKind.Utc).AddTicks(5739), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AN7Cko8cUBUT2XRb80UvgdqpH0Gag0YH6VWZb00+X/7mczcmxADSE1bcQ5Xp4iv6Uw==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
