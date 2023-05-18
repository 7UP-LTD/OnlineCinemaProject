using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCinema.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DicGenres",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "DicGenres",
                newName: "Description");
        }
    }
}
