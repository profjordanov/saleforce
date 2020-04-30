using Microsoft.EntityFrameworkCore.Migrations;

namespace Saleforce.Permissions.Api.Migrations
{
    public partial class OneToOneSampls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryApprovals",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserInfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryApprovals_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    DeliveryApprovalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_DeliveryApprovals_DeliveryApprovalId",
                        column: x => x.DeliveryApprovalId,
                        principalTable: "DeliveryApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryApprovalId",
                table: "Deliveries",
                column: "DeliveryApprovalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovals_UserInfoId",
                table: "DeliveryApprovals",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "DeliveryApprovals");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
