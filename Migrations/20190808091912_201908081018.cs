using Microsoft.EntityFrameworkCore.Migrations;

namespace PgccApi.Migrations
{
    public partial class _201908081018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rinks_Competitions_CompetitionId",
                table: "Rinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Rinks_Seasons_SeasonId",
                table: "Rinks");

            migrationBuilder.AlterColumn<long>(
                name: "SeasonId",
                table: "Rinks",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CompetitionId",
                table: "Rinks",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rinks_Competitions_CompetitionId",
                table: "Rinks",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rinks_Seasons_SeasonId",
                table: "Rinks",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rinks_Competitions_CompetitionId",
                table: "Rinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Rinks_Seasons_SeasonId",
                table: "Rinks");

            migrationBuilder.AlterColumn<long>(
                name: "SeasonId",
                table: "Rinks",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CompetitionId",
                table: "Rinks",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Rinks_Competitions_CompetitionId",
                table: "Rinks",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rinks_Seasons_SeasonId",
                table: "Rinks",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
