using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenchlyBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocationListFromTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationTag");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LocationId",
                table: "Tag",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Location_LocationId",
                table: "Tag",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Location_LocationId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_LocationId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "LocationTag",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTag", x => new { x.LocationsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_LocationTag_Location_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationTag_TagsId",
                table: "LocationTag",
                column: "TagsId");
        }
    }
}
