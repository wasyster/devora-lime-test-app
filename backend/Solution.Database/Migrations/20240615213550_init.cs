using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arena", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FightHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArenaId = table.Column<int>(type: "int", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightHistory_Arena_ArenaId",
                        column: x => x.ArenaId,
                        principalTable: "Arena",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArenaId = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDead = table.Column<bool>(type: "bit", nullable: false),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hero_Arena_ArenaId",
                        column: x => x.ArenaId,
                        principalTable: "Arena",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FightHistory_ArenaId",
                table: "FightHistory",
                column: "ArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_ArenaId",
                table: "Hero",
                column: "ArenaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FightHistory");

            migrationBuilder.DropTable(
                name: "Hero");

            migrationBuilder.DropTable(
                name: "Arena");
        }
    }
}
