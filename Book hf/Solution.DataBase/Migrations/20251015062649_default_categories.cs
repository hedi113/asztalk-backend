using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class default_categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @$"
                insert into
                    [Category]
                    ([Name])
                values
                    ('Krimi'),
                    ('Sci-fi'),
                    ('Természettudomány'),
                    ('Történelem'),
                    ('Informatika'),
                    ('Mesekönyv'),
                    ('Szépirodalom'),
                    ('Thriller'),
                    ('Horror')
            ";

            migrationBuilder.Sql(query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $"truncate table [Category]";

            migrationBuilder.Sql(query);
        }
    }
}
