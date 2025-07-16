using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceOfInterest.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddingNameDescriptionToPlaceOfInterestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PlaceOfInterests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PlaceOfInterests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PlaceOfInterests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PlaceOfInterests");
        }
    }
}
