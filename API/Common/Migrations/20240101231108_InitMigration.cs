using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Common.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity.Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity.Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ShowFullName = table.Column<bool>(type: "INTEGER", nullable: true),
                    ProfilePicture = table.Column<string>(type: "TEXT", nullable: true),
                    ProfilePictureUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastLoggedIn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PasswordLastChanged = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DBProvider = table.Column<string>(type: "TEXT", nullable: true),
                    ConnectionString = table.Column<string>(type: "TEXT", nullable: true),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Identity.RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity.RoleClaims_Identity.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity.UserClaims_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Identity.UserLogins_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity.UserRoles_Identity.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity.UserRoles_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity.UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity.UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Identity.UserTokens_Identity.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Identity.Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastLoggedIn", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordLastChanged", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "ProfilePictureUpdated", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "ShowFullName", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"), 0, "9114e2e8-e617-4ff3-b2ef-4f792ecfe05e", "admin@rentsaas.com", false, "Admin", true, false, null, "Admin", false, null, null, null, "AOMnghLlRymBHoUEd9t+5I5+/TRHtqKioYpm7PMLmSWsPl9qyekZThR4j3cWq0801A==", null, null, false, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "ConnectionString", "DBProvider", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "RentSaaS", "Data Source=localhost;Initial Catalog=RentSaaS;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true", "MSSQL", true, "RentSaaS is a Default & Shared Database " },
                    { "SkyRealty", null, null, false, "Sky Realty" },
                    { "SkyRealty 3", null, null, false, "Sky Realty 3" },
                    { "SkyRealty1", "Data Source=localhost;Initial Catalog=SkyRealty1;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true", "MSSQL", false, "Sky Realty1" },
                    { "SkyRealty2", "Data Source=localhost;Initial Catalog=SkyRealty2;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true", "MSSQL", false, "Sky Realty2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Identity.RoleClaims_RoleId",
                table: "Identity.RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Identity.Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserClaims_UserId",
                table: "Identity.UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserLogins_UserId",
                table: "Identity.UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity.UserRoles_RoleId",
                table: "Identity.UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Identity.Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Identity.Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity.RoleClaims");

            migrationBuilder.DropTable(
                name: "Identity.UserClaims");

            migrationBuilder.DropTable(
                name: "Identity.UserLogins");

            migrationBuilder.DropTable(
                name: "Identity.UserRoles");

            migrationBuilder.DropTable(
                name: "Identity.UserTokens");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Identity.Roles");

            migrationBuilder.DropTable(
                name: "Identity.Users");
        }
    }
}
