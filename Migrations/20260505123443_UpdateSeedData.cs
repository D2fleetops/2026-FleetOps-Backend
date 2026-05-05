using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fleetops_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "trip_details",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "driver_image_url_path", "item_image_url_path", "waktu" },
                values: new object[] { "https://example.com/driver-image.jpg", "https://example.com/item-image.jpg", 7200 });

            migrationBuilder.UpdateData(
                table: "trips",
                keyColumn: "trip_id",
                keyValue: 1,
                columns: new[] { "catatan", "jarak_tempuh", "odometer_akhir", "odometer_awal" },
                values: new object[] { "Trip completed successfully", 150.0, 1150.5, 1000.5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "trip_details",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "driver_image_url_path", "item_image_url_path", "waktu" },
                values: new object[] { null, null, 120 });

            migrationBuilder.UpdateData(
                table: "trips",
                keyColumn: "trip_id",
                keyValue: 1,
                columns: new[] { "catatan", "jarak_tempuh", "odometer_akhir", "odometer_awal" },
                values: new object[] { null, null, null, null });
        }
    }
}
