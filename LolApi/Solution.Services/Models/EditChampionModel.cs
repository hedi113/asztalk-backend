
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Services.Models;

public class EditChampionModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string Role { get; set; }

    [Required]
    [StringLength(50)]
    public string Lane { get; set; }

    [Required]
    public int Difficulity { get; set; }

    [Required]
    public int BlueEssence { get; set; }

    [Required]
    [StringLength(50)]
    public string DamageType { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; }
}
