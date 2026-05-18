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
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Homeworld = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Era = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    LightSaberColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Master = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apprentice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ForceSpeciality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
