using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'08522cba-d6b6-4861-94a0-ba42269a21e2', N'Admin', N'ADMIN', N'fa8e22c3-c6d8-46e2-b150-5f36d1247d21')" +
                "INSERT INTO[dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(N'5c540f53-1590-45b2-86eb-0a831a2dcfab', N'Child', N'CHILD', N'a7f9c326-9028-4fb5-8b8a-975c78d3f832')" +
                "INSERT INTO[dbo].[AspNetRoles]([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES(N'ea457112-8f53-4712-9af7-e562f53172ba', N'Adult', N'ADULT', N'b4a7c7e4-8aa5-462e-91e6-01db7bdbd835')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
