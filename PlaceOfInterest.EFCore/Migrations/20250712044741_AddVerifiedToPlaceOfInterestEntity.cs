using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceOfInterest.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddVerifiedToPlaceOfInterestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "PlaceOfInterests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "PlaceOfInterests");
        }
    }
}
