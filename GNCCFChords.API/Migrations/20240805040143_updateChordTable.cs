using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GNCCFChords.API.Migrations
{
    /// <inheritdoc />
    public partial class updateChordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChordPartName",
                table: "ChordParts",
                newName: "IntroChords");

            migrationBuilder.RenameColumn(
                name: "ChordContent",
                table: "ChordParts",
                newName: "ChorusChords");

            migrationBuilder.AddColumn<string>(
                name: "BridgeChords",
                table: "ChordParts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreChorusChords",
                table: "ChordParts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BridgeChords",
                table: "ChordParts");

            migrationBuilder.DropColumn(
                name: "PreChorusChords",
                table: "ChordParts");

            migrationBuilder.RenameColumn(
                name: "IntroChords",
                table: "ChordParts",
                newName: "ChordPartName");

            migrationBuilder.RenameColumn(
                name: "ChorusChords",
                table: "ChordParts",
                newName: "ChordContent");
        }
    }
}
