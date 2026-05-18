using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Services.Models;

public class RouteUpdateModel
{
    public int Id { get; set; }

    public string DepartureCity { get; set; }

    public string ArrivalCity { get; set; }

    public int DepartureHour { get; set; }

    public int DepartureMinute { get; set; }

    public int ArrivalHour { get; set; }

    public int ArrivalMinute { get; set; }

    public int DistanceKm { get; set; }
}
