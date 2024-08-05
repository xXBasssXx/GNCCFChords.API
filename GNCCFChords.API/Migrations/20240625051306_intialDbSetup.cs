using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GNCCFChords.API.Migrations
{
    /// <inheritdoc />
    public partial class intialDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                });

            migrationBuilder.CreateTable(
                name: "ChordParts",
                columns: table => new
                {
                    ChordPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChordPartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChordContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChordKey = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChordParts", x => x.ChordPartId);
                    table.ForeignKey(
                        name: "FK_ChordParts_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LyricParts",
                columns: table => new
                {
                    LyricPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LyricPartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LyricContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LyricParts", x => x.LyricPartId);
                    table.ForeignKey(
                        name: "FK_LyricParts_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChordParts_SongId",
                table: "ChordParts",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_LyricParts_SongId",
                table: "LyricParts",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChordParts");

            migrationBuilder.DropTable(
                name: "LyricParts");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
