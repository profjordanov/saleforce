using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Saleforce.Permissions.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionType);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleType);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleType = table.Column<string>(nullable: false),
                    PermissionType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleType, x.PermissionType });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionType",
                        column: x => x.PermissionType,
                        principalTable: "Permissions",
                        principalColumn: "PermissionType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleType",
                        column: x => x.RoleType,
                        principalTable: "Roles",
                        principalColumn: "RoleType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Connector = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    DataScope = table.Column<string>(type: "jsonb", nullable: false),
                    RoleType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleType",
                        column: x => x.RoleType,
                        principalTable: "Roles",
                        principalColumn: "RoleType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Connector = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    DataScope = table.Column<string>(type: "jsonb", nullable: false),
                    PermissionType = table.Column<string>(nullable: false),
                    UserRolesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionType",
                        column: x => x.PermissionType,
                        principalTable: "Permissions",
                        principalColumn: "PermissionType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_UserRoles_UserRolesId",
                        column: x => x.UserRolesId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionType",
                table: "RolePermissions",
                column: "PermissionType");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionType",
                table: "UserPermissions",
                column: "PermissionType");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserRolesId",
                table: "UserPermissions",
                column: "UserRolesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleType",
                table: "UserRoles",
                column: "RoleType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
