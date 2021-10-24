using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class article_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_articles_UserId",
                table: "articles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_Users_UserId",
                table: "articles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_Users_UserId",
                table: "articles");

            migrationBuilder.DropIndex(
                name: "IX_articles_UserId",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "articles");
        }
    }
}
