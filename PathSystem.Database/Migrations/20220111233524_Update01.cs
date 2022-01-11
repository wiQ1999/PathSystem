using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PathSystem.Database.Migrations
{
    public partial class Update01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PathPoints");

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "Paths",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Paths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Paths",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Entities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Entities",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntitiesPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitiesPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitiesPosition_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PathPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    Milliseconds = table.Column<int>(type: "int", nullable: false),
                    PathModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathPositions_Paths_PathModelId",
                        column: x => x.PathModelId,
                        principalTable: "Paths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paths_EntityId",
                table: "Paths",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitiesPosition_EntityId",
                table: "EntitiesPosition",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PathPositions_PathModelId",
                table: "PathPositions",
                column: "PathModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paths_Entities_EntityId",
                table: "Paths",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paths_Entities_EntityId",
                table: "Paths");

            migrationBuilder.DropTable(
                name: "EntitiesPosition");

            migrationBuilder.DropTable(
                name: "PathPositions");

            migrationBuilder.DropIndex(
                name: "IX_Paths_EntityId",
                table: "Paths");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Paths");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Paths");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Entities");

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
                table: "Paths",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PathPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathModelId = table.Column<int>(type: "int", nullable: true),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    VisitTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathPoints_Paths_PathModelId",
                        column: x => x.PathModelId,
                        principalTable: "Paths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PathPoints_PathModelId",
                table: "PathPoints",
                column: "PathModelId");
        }
    }
}
