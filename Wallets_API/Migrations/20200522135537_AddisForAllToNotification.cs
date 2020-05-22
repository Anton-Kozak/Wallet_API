using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class AddisForAllToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isForAll",
                table: "Notifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isForAll",
                table: "Notifications");
        }
    }
}
