using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCinema.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditBannerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isLike",
                table: "UserMovieLikes",
                newName: "IsLike");

            migrationBuilder.AlterColumn<string>(
                name: "MovieBannerUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLike",
                table: "UserMovieLikes",
                newName: "isLike");

            migrationBuilder.AlterColumn<string>(
                name: "MovieBannerUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
