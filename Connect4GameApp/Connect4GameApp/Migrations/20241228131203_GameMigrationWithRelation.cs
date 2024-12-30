using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connect4GameApp.Migrations
{
    /// <inheritdoc />
    public partial class GameMigrationWithRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    HostId = table.Column<string>(type: "TEXT", nullable: false),
                    GuestId = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardColor = table.Column<string>(type: "TEXT", nullable: false),
                    Player1Color = table.Column<string>(type: "TEXT", nullable: false),
                    Player2Color = table.Column<string>(type: "TEXT", nullable: false),
                    GridSize = table.Column<int>(type: "INTEGER", nullable: false),
                    GridState = table.Column<string>(type: "TEXT", nullable: false),
                    HostPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestPoints = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_AspNetUsers_GuestId",
                        column: x => x.GuestId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Games_AspNetUsers_HostId",
                        column: x => x.HostId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GuestId",
                table: "Games",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HostId",
                table: "Games",
                column: "HostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
