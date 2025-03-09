using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAdvertising : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("1708c1af-6002-462f-9ca8-a67b15f4018b"));

            migrationBuilder.DropColumn(
                name: "Leads",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Advertising");

            migrationBuilder.AddColumn<bool>(
                name: "Apartments",
                table: "Advertising",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvailableForm",
                table: "Advertising",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Advertising",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MontholyRent",
                table: "Advertising",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Realtor",
                table: "Advertising",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptsFiles",
                table: "Advertising",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDeposit",
                table: "Advertising",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Trulia",
                table: "Advertising",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Zillow",
                table: "Advertising",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvertisingFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdvertisingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisingFiles_Advertising_AdvertisingId",
                        column: x => x.AdvertisingId,
                        principalTable: "Advertising",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("acd79ef2-9744-449e-9d94-610ed0feacf5"), 0, "92614f78-f133-4028-aff4-9dfb5715fec8", new DateTime(2025, 3, 9, 19, 40, 0, 199, DateTimeKind.Utc).AddTicks(783), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AKbYLPi0YBrRfZIrc3IRnw+z4u0P+IkQVcSOegoZsXn8dp/uIJ40a06IC3lJteOcrQ==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisingFiles_AdvertisingId",
                table: "AdvertisingFiles",
                column: "AdvertisingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisingFiles");

            migrationBuilder.DeleteData(
                table: "Identity.Users",
                keyColumn: "Id",
                keyValue: new Guid("acd79ef2-9744-449e-9d94-610ed0feacf5"));

            migrationBuilder.DropColumn(
                name: "Apartments",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "AvailableForm",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "MontholyRent",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Realtor",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "ReceiptsFiles",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "SecurityDeposit",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Trulia",
                table: "Advertising");

            migrationBuilder.DropColumn(
                name: "Zillow",
                table: "Advertising");

            migrationBuilder.AddColumn<int>(
                name: "Leads",
                table: "Advertising",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Advertising",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Advertising",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastModifiedAt", "LastModifiedBy", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("1708c1af-6002-462f-9ca8-a67b15f4018b"), 0, "60424c94-59ac-457a-90fd-3e39f5f135f0", new DateTime(2025, 3, 8, 11, 20, 29, 840, DateTimeKind.Utc).AddTicks(4738), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "admin@rentsaas.com", false, "Admin", true, false, null, null, null, "Admin", false, null, null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), "AMRCsj/BtGexoteclrjTSwz39W6hXGQFz8czSpzxEuUhZjHyn7tvwOuINKV2htiw1Q==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin", "Landlord" });
        }
    }
}
