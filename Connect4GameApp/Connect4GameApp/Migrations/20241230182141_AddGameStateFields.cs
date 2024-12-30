using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connect4GameApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGameStateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerTurn",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastPlayer",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPlayerTurn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastPlayer",
                table: "Games");
        }
    }
}
