namespace Solution.Database.Entities;

[Table("Category")]
[Index(nameof(Name), IsUnique = true)]
public class CategoryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public string Name { get; set; }

    public virtual ICollection<BookEntity> Books { get; set; }
}
