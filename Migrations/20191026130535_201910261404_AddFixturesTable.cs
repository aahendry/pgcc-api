using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PgccApi.Migrations
{
    public partial class _201910261404_AddFixturesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Blurb",
                table: "Competitions",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "HasLeagueTable",
                table: "Competitions",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SeasonId = table.Column<long>(nullable: false),
                    CompetitionId = table.Column<long>(nullable: false),
                    Team1Id = table.Column<long>(nullable: true),
                    Team2Id = table.Column<long>(nullable: true),
                    Team1OtherName = table.Column<string>(nullable: true),
                    Team2OtherName = table.Column<string>(nullable: true),
                    Shots1 = table.Column<int>(nullable: true),
                    Shots2 = table.Column<int>(nullable: true),
                    Ends1 = table.Column<int>(nullable: true),
                    Ends2 = table.Column<int>(nullable: true),
                    When = table.Column<DateTime>(nullable: false),
                    Round = table.Column<string>(nullable: true),
                    isFinal = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixtures_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fixtures_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fixtures_Rinks_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Rinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Rinks_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Rinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_CompetitionId",
                table: "Fixtures",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_SeasonId",
                table: "Fixtures",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_Team1Id",
                table: "Fixtures",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_Team2Id",
                table: "Fixtures",
                column: "Team2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropColumn(
                name: "Blurb",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "HasLeagueTable",
                table: "Competitions");
        }
    }
}
