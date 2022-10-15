using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigHub.Migrations
{
    public partial class PopulateGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Pop')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Rock')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Jazz')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Country')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Genres");
        }
    }
}
