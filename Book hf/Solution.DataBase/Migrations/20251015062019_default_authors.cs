using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class default_authors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @$"
                insert into
                    [Author]
                    ([Name],[BirthYear])
                values
                    ('Christie Agatha',1890),
                    ('Fable	Vavyan',1956),
                    ('King Stephen',1947),
                    ('Harrison Harry',1925),
                    ('Asimov Isaac',1920),
                    ('Castelot André',1911)
            ";

            migrationBuilder.Sql(query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = $"truncate table [Author]";

            migrationBuilder.Sql(query);
        }
    }
}
