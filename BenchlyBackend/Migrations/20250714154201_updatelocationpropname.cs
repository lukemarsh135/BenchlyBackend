using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenchlyBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatelocationpropname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistanceFromCurrentLocation",
                table: "Location",
                newName: "DistanceFromCurrentLocationMiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistanceFromCurrentLocationMiles",
                table: "Location",
                newName: "DistanceFromCurrentLocation");
        }
    }
}
