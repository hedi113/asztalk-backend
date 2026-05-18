using System;
using System.Collections.Generic;
using System.Text;

namespace TurkeyRoutesConsoleApp;

public class Route
{
    public int Id { get; set; }

    public string DepartureCity { get; set; }

    public string ArrivalCity { get; set; }

    public int DepartureHour { get; set; }

    public int DepartureMinute { get; set; }

    public int ArrivalHour { get; set; }

    public int ArrivalMinute { get; set; }

    public int DistanceKm { get; set; }

    public int DurrationInMinues => new TimeSpan(DepartureHour, DepartureMinute, 0).Subtract(new TimeSpan(DepartureHour, DepartureMinute, 0)).Minutes;
}
