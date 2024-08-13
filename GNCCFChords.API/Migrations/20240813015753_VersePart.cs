using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GNCCFChords.API.Migrations
{
    /// <inheritdoc />
    public partial class VersePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Verse",
                table: "ChordParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verse",
                table: "ChordParts");
        }
    }
}
