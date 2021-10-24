using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class updatedb_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobName",
                table: "Business");

            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "Business",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Business_JobId",
                table: "Business",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Jobs_JobId",
                table: "Business",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_Jobs_JobId",
                table: "Business");

            migrationBuilder.DropIndex(
                name: "IX_Business_JobId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Business");

            migrationBuilder.AddColumn<string>(
                name: "JobName",
                table: "Business",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
