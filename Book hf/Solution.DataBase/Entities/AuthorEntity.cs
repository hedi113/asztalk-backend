namespace Solution.Database.Entities;

[Table("Author")]
[Index(nameof(Name), IsUnique = true)]
public class AuthorEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public string Name { get; set; }

    [Required]
    public int BirthYear { get; set; }

    public virtual ICollection<BookEntity> Books { get; set; }
}
