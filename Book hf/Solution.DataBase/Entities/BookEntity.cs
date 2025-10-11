namespace Solution.Database.Entities;

[Table("Book")]
public class BookEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(128)]
    [Required]
    public string PublicId { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [StringLength(128)]
    public string? ImageId { get; set; }

    [StringLength(512)]
    public string? WebContentLink { get; set; }

    [Required]
    public int PageNumber { get; set; }

    [Required]
    [StringLength(30)]
    public string Publisher { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [ForeignKey("AuthorEntity")]
    public int AuthorId { get; set; }
    public virtual AuthorEntity Author { get; set; }

    [ForeignKey("CategoryEntity")]
    public int CategoryId { get; set; }
    public virtual CategoryEntity Category { get; set; }

}
