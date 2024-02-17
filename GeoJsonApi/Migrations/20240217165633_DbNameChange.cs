using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoJsonApi.Migrations
{
    /// <inheritdoc />
    public partial class DbNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpatialDatas",
                table: "SpatialDatas");

            migrationBuilder.RenameTable(
                name: "SpatialDatas",
                newName: "spatialdatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_spatialdatas",
                table: "spatialdatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_spatialdatas",
                table: "spatialdatas");

            migrationBuilder.RenameTable(
                name: "spatialdatas",
                newName: "SpatialDatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpatialDatas",
                table: "SpatialDatas",
                column: "Id");
        }
    }
}
