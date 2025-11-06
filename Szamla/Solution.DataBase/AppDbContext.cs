namespace Solution.Services;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }

	public DbSet<InvoiceEntity> Invoices { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
}
