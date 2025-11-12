namespace Solution.Database.Entities;

[Table("Invoice")]
[Index(nameof(InvoiceNumber), IsUnique = true)]
public class InvoiceEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(24)]
    [Required]
    public string InvoiceNumber { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    [Required]
    public int SumOfInvoiceItemValues { get; set; }

    public virtual ICollection<InvoiceItemEntity> InvoiceItems { get; set; }
}
