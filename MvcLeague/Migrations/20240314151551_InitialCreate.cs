using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLeague.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teamId = table.Column<int>(type: "int", nullable: false),
                    playerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    teamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teamName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    league = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    throphies = table.Column<int>(type: "int", nullable: true),
                    marketValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.teamId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_userName",
                table: "User",
                column: "userName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
