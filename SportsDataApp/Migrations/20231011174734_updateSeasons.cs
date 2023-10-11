using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsDataApp.Migrations
{
    public partial class updateSeasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "winPercent",
                table: "Season",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "winPercent",
                table: "Season");
        }
    }
}
