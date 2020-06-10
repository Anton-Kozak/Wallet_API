using Microsoft.EntityFrameworkCore.Migrations;

namespace Wallets_API.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
"SET IDENTITY_INSERT [dbo].[ExpenseCategories] ON INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (1, N'Food') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (2, N'Vegetables') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (3, N'Alcohol') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (4, N'Meat') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (5, N'Sweets') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (6, N'Fast food') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (7, N'Fruits') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (8, N'Housekeeping') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (9, N'Electricity')INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (10, N'Gas') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (11, N'Water') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (12, N'Entertainment') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (13, N'Internet shopping') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (14, N'Restaurants') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (15, N'Cinema') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (16, N'Theatre') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (17, N'Smoking') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (18, N'Medicine') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (19, N'Medicaments') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (20, N'Treatment') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (21, N'Beauty') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (22, N'Beauty accessories') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (23, N'Beauty products') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (24, N'Beauty procedures') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (25, N'Sport') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (26, N'Sport events') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (27, N'Sport gambling') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (28, N'Sport inventory') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (29, N'Sport activities') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (30, N'Transportation') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (31, N'Air transportation') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (32, N'Land transportation') INSERT INTO [dbo].[ExpenseCategories] ([Id], [Title]) VALUES (33, N'Other') SET IDENTITY_INSERT [dbo].[ExpenseCategories] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
