using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class AddFieldsApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWalletAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WalletID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWalletAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletID",
                table: "AspNetUsers");
        }
    }
}
