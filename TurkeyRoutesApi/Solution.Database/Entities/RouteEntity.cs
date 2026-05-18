using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

public class RouteEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string DepartureCity { get; set; }

    [Required]
    [StringLength(50)]
    public string ArrivalCity { get; set; }

    [Required]
    public int DepartureHour { get; set; }

    [Required]
    public int DepartureMinute { get; set; }

    [Required]
    public int ArrivalHour { get; set; }

    [Required]
    public int ArrivalMinute { get; set; }

    [Required]
    public int DistanceKm { get; set; }
}
