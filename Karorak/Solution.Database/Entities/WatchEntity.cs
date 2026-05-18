using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

public class WatchEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Manufacturer { get; set; }

    [Required]
    [StringLength(50)]
    public string Model { get; set; }

    [Required]
    public int ReleaseYear { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; }

    [Required]
    [StringLength(50)]
    public string Movement { get; set; }

    [Required]
    public int WaterResistanceM { get; set; }

    [Required]
    [StringLength(50)]
    public string CaseMaterial { get; set; }

    [Required]
    [StringLength(50)]
    public string Functions { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; }
}
