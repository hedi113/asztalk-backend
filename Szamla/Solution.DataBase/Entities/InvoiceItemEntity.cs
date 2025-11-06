namespace Solution.Database.Entities;

[Table("InvoiceItem")]
public class InvoiceItemEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50)]
    [Required]
    public string Name { get; set; }

    [Required]
    public int UnitPrice { get; set; }

    [Required]
    public int Quantity { get; set; }

    public int InvoiceId { get; set; }
    public virtual InvoiceEntity Invoice { get; set; }
}
