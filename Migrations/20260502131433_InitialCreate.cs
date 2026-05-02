using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fleetops_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "inspection_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    jenis_lisensi = table.Column<string>(type: "text", nullable: true),
                    tanggal_berlaku = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_drivers", x => x.driver_id);
                    table.ForeignKey(
                        name: "fk_drivers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_name = table.Column<string>(type: "text", nullable: false),
                    p_lat_nomor = table.Column<string>(type: "text", nullable: false),
                    odometer = table.Column<int>(type: "integer", nullable: false),
                    tipe = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    maintenance_interval_km = table.Column<int>(type: "integer", nullable: false),
                    maintenance_interval_day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicles", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "fk_vehicles_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fuels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    driver_id = table.Column<int>(type: "integer", nullable: true),
                    jml_liter = table.Column<int>(type: "integer", nullable: false),
                    harga_liter = table.Column<int>(type: "integer", nullable: false),
                    harga_total = table.Column<int>(type: "integer", nullable: false),
                    lokasi_pengisian = table.Column<string>(type: "text", nullable: true),
                    image_url_path = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fuels", x => x.id);
                    table.ForeignKey(
                        name: "fk_fuels_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_fuels_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "maintenances",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    tanggal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    catatan = table.Column<string>(type: "text", nullable: false),
                    odometer = table.Column<int>(type: "integer", nullable: true),
                    biaya = table.Column<decimal>(type: "numeric", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_maintenances", x => x.id);
                    table.ForeignKey(
                        name: "fk_maintenances_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    kord_awal = table.Column<Point>(type: "geography (Point, 4326)", nullable: true),
                    kord_akhir = table.Column<Point>(type: "geography (Point, 4326)", nullable: true),
                    lokasi_awal = table.Column<string>(type: "text", nullable: true),
                    lokasi_akhir = table.Column<string>(type: "text", nullable: true),
                    waktu_mulai = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    waktu_selesai = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trips", x => x.trip_id);
                    table.ForeignKey(
                        name: "fk_trips_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trips_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "inspections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    trip_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspections", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspections_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inspections_trips_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trips",
                        principalColumn: "trip_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inspections_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trip_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trip_id = table.Column<int>(type: "integer", nullable: false),
                    jarak = table.Column<int>(type: "integer", nullable: false),
                    avg_speed = table.Column<int>(type: "integer", nullable: false),
                    waktu = table.Column<int>(type: "integer", nullable: false),
                    image_url_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trip_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_trip_details_trips_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trips",
                        principalColumn: "trip_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspection_photos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inspection_id = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    image_url_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspection_photos_inspections_inspection_id",
                        column: x => x.inspection_id,
                        principalTable: "inspections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inspection_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inspection_id = table.Column<int>(type: "integer", nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    photo_id = table.Column<int>(type: "integer", nullable: true),
                    condition = table.Column<int>(type: "integer", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspection_results", x => x.id);
                    table.ForeignKey(
                        name: "fk_inspection_results_inspection_items_item_id",
                        column: x => x.item_id,
                        principalTable: "inspection_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_inspection_results_inspection_photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "inspection_photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_inspection_results_inspections_inspection_id",
                        column: x => x.inspection_id,
                        principalTable: "inspections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "inspection_items",
                columns: new[] { "id", "is_required", "name" },
                values: new object[,]
                {
                    { 1, true, "Brake Condition" },
                    { 2, true, "Tire Condition" },
                    { 3, true, "Lights" },
                    { 4, false, "Mirrors" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "FleetManager" },
                    { 3, "Driver" },
                    { 4, "Employee" },
                    { 5, "Unassigned" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "password_hash", "role_id" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2026, 5, 2, 11, 59, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "driver@fleetops.local", "FleetOps Test Driver", "$2a$11$wvBoSd9TRmWoarbcfmSrwu.AeqYvmWSP1o.fdUrj88yS33TSJL2Dm", 3 });

            migrationBuilder.InsertData(
                table: "drivers",
                columns: new[] { "driver_id", "jenis_lisensi", "status", "tanggal_berlaku", "user_id" },
                values: new object[] { 1, "B", "Active", new DateTimeOffset(new DateTime(2031, 5, 2, 11, 59, 13, 649, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 });

            migrationBuilder.InsertData(
                table: "vehicles",
                columns: new[] { "vehicle_id", "driver_id", "maintenance_interval_day", "maintenance_interval_km", "odometer", "p_lat_nomor", "status", "tipe", "vehicle_name" },
                values: new object[] { 1, 1, 90, 5000, 0, "B 1234 ABC", "Active", "Truck", "Truck-001" });

            migrationBuilder.InsertData(
                table: "fuels",
                columns: new[] { "id", "created_at", "driver_id", "harga_liter", "harga_total", "image_url_path", "jml_liter", "lokasi_pengisian", "vehicle_id" },
                values: new object[] { 1, new DateTime(2026, 5, 2, 11, 59, 13, 651, DateTimeKind.Utc), 1, 15000, 750000, null, 50, "Jakarta Pusat", 1 });

            migrationBuilder.InsertData(
                table: "maintenances",
                columns: new[] { "id", "biaya", "catatan", "created_at", "odometer", "tanggal", "vehicle_id" },
                values: new object[] { 1, 500000m, "Rutin maintenance check", new DateTimeOffset(new DateTime(2026, 5, 2, 11, 59, 13, 651, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 5000, new DateTime(2026, 5, 2, 11, 59, 13, 651, DateTimeKind.Utc), 1 });

            migrationBuilder.InsertData(
                table: "trips",
                columns: new[] { "trip_id", "driver_id", "kord_akhir", "kord_awal", "lokasi_akhir", "lokasi_awal", "status", "vehicle_id", "waktu_mulai", "waktu_selesai" },
                values: new object[] { 1, 1, null, null, "Bandung", "Jakarta", "Completed", 1, new DateTimeOffset(new DateTime(2026, 5, 2, 9, 59, 13, 650, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 2, 11, 59, 13, 650, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "inspections",
                columns: new[] { "id", "created_at", "driver_id", "status", "trip_id", "vehicle_id" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2026, 5, 2, 11, 59, 13, 652, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, true, 1, 1 });

            migrationBuilder.InsertData(
                table: "trip_details",
                columns: new[] { "id", "avg_speed", "image_url_path", "jarak", "trip_id", "waktu" },
                values: new object[] { 1, 75, null, 150, 1, 120 });

            migrationBuilder.InsertData(
                table: "inspection_photos",
                columns: new[] { "id", "image_url_path", "inspection_id", "is_required" },
                values: new object[] { 1, "https://example.com/photo1.jpg", 1, true });

            migrationBuilder.InsertData(
                table: "inspection_results",
                columns: new[] { "id", "condition", "inspection_id", "item_id", "note", "photo_id" },
                values: new object[,]
                {
                    { 2, 1, 1, 2, "Tires properly inflated", null },
                    { 1, 1, 1, 1, "Brake pads good condition", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_drivers_user_id",
                table: "drivers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fuels_driver_id",
                table: "fuels",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_fuels_vehicle_id",
                table: "fuels",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_photos_inspection_id",
                table: "inspection_photos",
                column: "inspection_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_results_inspection_id",
                table: "inspection_results",
                column: "inspection_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_results_item_id",
                table: "inspection_results",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspection_results_photo_id",
                table: "inspection_results",
                column: "photo_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspections_driver_id",
                table: "inspections",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspections_trip_id",
                table: "inspections",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "ix_inspections_vehicle_id",
                table: "inspections",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_maintenances_vehicle_id",
                table: "maintenances",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_trip_details_trip_id",
                table: "trip_details",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_driver_id",
                table: "trips",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "ix_trips_vehicle_id",
                table: "trips",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_vehicles_driver_id",
                table: "vehicles",
                column: "driver_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fuels");

            migrationBuilder.DropTable(
                name: "inspection_results");

            migrationBuilder.DropTable(
                name: "maintenances");

            migrationBuilder.DropTable(
                name: "trip_details");

            migrationBuilder.DropTable(
                name: "inspection_items");

            migrationBuilder.DropTable(
                name: "inspection_photos");

            migrationBuilder.DropTable(
                name: "inspections");

            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "drivers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
