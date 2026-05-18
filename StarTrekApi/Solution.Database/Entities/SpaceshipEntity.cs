using Solution.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solution.Database.Entities;

public class SpaceshipEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Class { get; set; }

    [Required]
    [StringLength(50)]
    public string RaceFaction { get; set; }

    [Required]
    public int Length { get; set; }

    [Required]
    public int Crew { get; set; }

    [Required]
    public double MaxWarp { get; set; }

    [Required]
    [StringLength(50)]
    public string Armament { get; set; }
    
    [Required]
    [StringLength(50)]
    public string ShieldType { get; set; }

    [Required]
    [StringLength(50)]
    public string HullMaterial { get; set; }

    [Required]
    public ShipRoleEnum Role { get; set; }
}
