using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'08522cba-d6b6-4861-94a0-ba42269a21e2', N'Admin', N'ADMIN', N'fa8e22c3-c6d8-46e2-b150-5f36d1247d21')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
