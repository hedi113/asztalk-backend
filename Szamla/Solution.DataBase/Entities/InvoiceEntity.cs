namespace Solution.Database.Entities;

[Table("Invoice")]
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
}
