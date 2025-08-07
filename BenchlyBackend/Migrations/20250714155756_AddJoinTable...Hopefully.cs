using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenchlyBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinTableHopefully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Location_LocationId",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Location_LocationId",
                table: "Review",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Location_LocationId",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Location_LocationId",
                table: "Review",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
