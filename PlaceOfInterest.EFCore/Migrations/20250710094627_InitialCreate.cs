using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceOfInterest.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StartLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceOfInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicUniqueToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerrainScore = table.Column<byte>(type: "tinyint", nullable: false),
                    Score = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceOfInterests_EndLocations_EndLocationId",
                        column: x => x.EndLocationId,
                        principalTable: "EndLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceOfInterests_StartLocations_StartLocationId",
                        column: x => x.StartLocationId,
                        principalTable: "StartLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOfInterests_EndLocationId",
                table: "PlaceOfInterests",
                column: "EndLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOfInterests_StartLocationId",
                table: "PlaceOfInterests",
                column: "StartLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceOfInterests");

            migrationBuilder.DropTable(
                name: "EndLocations");

            migrationBuilder.DropTable(
                name: "StartLocations");
        }
    }
}
