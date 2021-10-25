using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class updatedb_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "WorkChecks");

            migrationBuilder.AddColumn<string>(
                name: "WorkStatusId",
                table: "WorkChecks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkStatus",
                columns: table => new
                {
                    WorkStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStatus", x => x.WorkStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkChecks_WorkStatusId",
                table: "WorkChecks",
                column: "WorkStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkChecks_WorkStatus_WorkStatusId",
                table: "WorkChecks",
                column: "WorkStatusId",
                principalTable: "WorkStatus",
                principalColumn: "WorkStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkChecks_WorkStatus_WorkStatusId",
                table: "WorkChecks");

            migrationBuilder.DropTable(
                name: "WorkStatus");

            migrationBuilder.DropIndex(
                name: "IX_WorkChecks_WorkStatusId",
                table: "WorkChecks");

            migrationBuilder.DropColumn(
                name: "WorkStatusId",
                table: "WorkChecks");

            migrationBuilder.AddColumn<string>(
                name: "WorkStatus",
                table: "WorkChecks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
