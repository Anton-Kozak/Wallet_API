using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class AddTargetUserToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetUser",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetUser",
                table: "Notifications");
        }
    }
}
