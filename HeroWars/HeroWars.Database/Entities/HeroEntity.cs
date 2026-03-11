using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HeroWars.Database.Entities;

public class HeroEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public int Role { get; set; }
    [Required]
    public int Agility { get; set; }
    [Required]
    public int Strength { get; set; }
    [Required]
    public int Health { get; set; }
    [Required]
    public int PhysicalAttack { get; set; }
    [Required]
    public int MagicAttack { get; set; }
    [Required]
    public int Armor { get; set; }
    [Required]
    public int MagicDefense { get; set; }
    [Required]
    public int MagicPenetration { get; set; }
    [Required]
    public int ArmorPenetration { get; set; }
}
