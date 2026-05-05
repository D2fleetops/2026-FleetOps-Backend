using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fleetops_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTripTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_url_path",
                table: "trip_details",
                newName: "item_image_url_path");

            migrationBuilder.AddColumn<string>(
                name: "catatan",
                table: "trips",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "jarak_tempuh",
                table: "trips",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "odometer_akhir",
                table: "trips",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "odometer_awal",
                table: "trips",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "catatan",
                table: "trip_details",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "driver_image_url_path",
                table: "trip_details",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "trip_details",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "catatan", "driver_image_url_path" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "trips",
                keyColumn: "trip_id",
                keyValue: 1,
                columns: new[] { "catatan", "jarak_tempuh", "odometer_akhir", "odometer_awal" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catatan",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "jarak_tempuh",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "odometer_akhir",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "odometer_awal",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "catatan",
                table: "trip_details");

            migrationBuilder.DropColumn(
                name: "driver_image_url_path",
                table: "trip_details");

            migrationBuilder.RenameColumn(
                name: "item_image_url_path",
                table: "trip_details",
                newName: "image_url_path");
        }
    }
}
