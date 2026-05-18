using Solution.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

public class CharacterEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    public OrderTypeEnum OrderType { get; set; }

    [Required]
    [StringLength(50)]
    public string Species { get; set; }

    [Required]
    [StringLength(50)]
    public string Homeworld { get; set; }

    [Required]
    public EraEnum Era { get; set; }

    [Required]
    public RankEnum Rank { get; set; }

    [Required]
    [StringLength(50)]
    public string LightSaberColor { get; set; }

    [Required]
    [StringLength(50)]
    public string Master { get; set; }

    [Required]
    [StringLength(50)]
    public string Apprentice { get; set; }

    [Required]
    [StringLength(50)]
    public string ForceSpeciality { get; set; }

    [Required]
    public bool IsAlive { get; set; }
}
