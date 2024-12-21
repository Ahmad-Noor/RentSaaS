using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Apartment = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    POBox = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ShowLogo = table.Column<bool>(type: "bit", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    CommercialNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SiteURL = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DefaultCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    SystemType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserLogin", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Identity.Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowFullName = table.Column<bool>(type: "bit", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoggedIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordLastChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "OrganizationId", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { 1L, 0, "ddde2499-8b41-4698-a3d9-1aee529fd8fd", null, "admin@rentsaas.com", false, "Admin", true, null, null, "Admin", false, null, null, null, null, "1", "AI5aVe5Uqe3jsofgbvU7LbdkYgQPn5cWKLkJOyo4miOjc2ALcUpSL4BmmZvz0xbkmg==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, "admin" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "CreatedAt", "IsActive", "IsDeleted", "Name", "Note", "UpdatedAt" },
                values: new object[] { 1L, new DateTime(2024, 12, 21, 15, 6, 19, 832, DateTimeKind.Local).AddTicks(7130), true, null, "Organization 1", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Identity.RoleClaims");

            migrationBuilder.DropTable(
                name: "Identity.Roles");

            migrationBuilder.DropTable(
                name: "Identity.UserClaims");

            migrationBuilder.DropTable(
                name: "Identity.UserLogin");

            migrationBuilder.DropTable(
                name: "Identity.UserRoles");

            migrationBuilder.DropTable(
                name: "Identity.Users");

            migrationBuilder.DropTable(
                name: "Identity.UserTokens");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
