using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fleetops_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCatatanFromTripDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catatan",
                table: "trip_details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "catatan",
                table: "trip_details",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "trip_details",
                keyColumn: "id",
                keyValue: 1,
                column: "catatan",
                value: null);
        }
    }
}
