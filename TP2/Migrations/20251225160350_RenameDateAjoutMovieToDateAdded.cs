using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP2.Migrations
{
    /// <inheritdoc />
    public partial class RenameDateAjoutMovieToDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserCarts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "DateAjoutMovie",
                table: "Movies",
                newName: "DateAdded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserCarts",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Movies",
                newName: "DateAjoutMovie");
        }
    }
}
