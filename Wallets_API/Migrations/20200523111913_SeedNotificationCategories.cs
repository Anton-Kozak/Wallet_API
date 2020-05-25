using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class SeedNotificationCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[NotificationCategories] ON " +
                "INSERT INTO [dbo].[NotificationCategories] ([Id], [Title]) VALUES (1, N'NewMember')" +
                "INSERT INTO [dbo].[NotificationCategories] ([Id], [Title]) VALUES (2, N'75')" +
                "INSERT INTO [dbo].[NotificationCategories] ([Id], [Title]) VALUES (3, N'90')" +
                "INSERT INTO [dbo].[NotificationCategories] ([Id], [Title]) VALUES (4, N'Limit')" +
                "INSERT INTO [dbo].[NotificationCategories] ([Id], [Title]) VALUES (5, N'NewMemberRequest')" +
                "INSERT INTO[dbo].[NotificationCategories]([Id], [Title]) VALUES(6, N'NewMemberInvite')" +
                "SET IDENTITY_INSERT [dbo].[NotificationCategories] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
