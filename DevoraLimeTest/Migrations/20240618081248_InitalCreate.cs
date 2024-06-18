using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevoraLimeTest.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfHeroes = table.Column<int>(type: "int", nullable: false),
                    inFight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arenas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfFights = table.Column<int>(type: "int", nullable: false),
                    ArenaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_Arenas_ArenaID",
                        column: x => x.ArenaID,
                        principalTable: "Arenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HP = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    ArenaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_Arenas_ArenaID",
                        column: x => x.ArenaID,
                        principalTable: "Arenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Heroes_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryID = table.Column<int>(type: "int", nullable: false),
                    AttackerID = table.Column<int>(type: "int", nullable: false),
                    AttackerStartHP = table.Column<int>(type: "int", nullable: false),
                    AttackerEndHP = table.Column<int>(type: "int", nullable: false),
                    DeffenderID = table.Column<int>(type: "int", nullable: false),
                    DeffenderStartHP = table.Column<int>(type: "int", nullable: false),
                    DeffenderEndHP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fights_Heroes_AttackerID",
                        column: x => x.AttackerID,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fights_Heroes_DeffenderID",
                        column: x => x.DeffenderID,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fights_History_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "History",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "MaxHP", "Name" },
                values: new object[] { 1, 100, "Archer" });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "MaxHP", "Name" },
                values: new object[] { 2, 150, "Knight" });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "MaxHP", "Name" },
                values: new object[] { 3, 120, "SwordsMan" });

            migrationBuilder.CreateIndex(
                name: "IX_Fights_AttackerID",
                table: "Fights",
                column: "AttackerID");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_DeffenderID",
                table: "Fights",
                column: "DeffenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_HistoryID",
                table: "Fights",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_ArenaID",
                table: "Heroes",
                column: "ArenaID");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_TypeID",
                table: "Heroes",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_History_ArenaID",
                table: "History",
                column: "ArenaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fights");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Arenas");
        }
    }
}
