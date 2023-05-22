using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCinema.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBannerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieBannerUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieBannerUrl",
                table: "Movies");
        }
    }
}
