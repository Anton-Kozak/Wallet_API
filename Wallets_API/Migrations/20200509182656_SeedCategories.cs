using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "SET IDENTITY_INSERT [dbo].[ExpenseCategories] ON " +
                "INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (1, N'Food')" +
                "INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (2, N'Housekeeping')" +
                "INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (3, N'Clothes')" +
                "INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (4, N'Entertainment')" +
                "INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (5, N'Other') " +
                "SET IDENTITY_INSERT [dbo].[ExpenseCategories] OFF"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
