using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArrivalCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartureHour = table.Column<int>(type: "int", nullable: false),
                    DepartureMinute = table.Column<int>(type: "int", nullable: false),
                    ArrivalHour = table.Column<int>(type: "int", nullable: false),
                    ArrivalMinute = table.Column<int>(type: "int", nullable: false),
                    DistanceKm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
