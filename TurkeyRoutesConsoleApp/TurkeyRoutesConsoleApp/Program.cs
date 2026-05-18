using TurkeyRoutesConsoleApp;

var routes = new List<Route>();

var file = File.ReadAllLines("adatok.csv").Skip(1);

foreach(var line in file)
{
    var data = line.Split(',');

    routes.Add(new Route
    {
        DepartureCity = data[0],
        ArrivalCity = data[1],
        DepartureHour = int.Parse(data[2]),
        DepartureMinute = int.Parse(data[3]),
        ArrivalHour = int.Parse(data[4]),
        ArrivalMinute = int.Parse(data[5]),
        DistanceKm = int.Parse(data[6]),
    });
}

//2. Határozd meg, hány út szerepel az adatállományban.

Console.WriteLine($"Utak száma: {routes.Count}");

//3. Városok listája (kiinduló és érkező városok).

Console.WriteLine("Kiinduló városok: ");

var departureCities = routes.DistinctBy(x => x.DepartureCity).Select(x => x.DepartureCity).ToList();
var arrivalCities = routes.DistinctBy(x => x.ArrivalCity).Select(x => x.ArrivalCity).ToList();
var allCities = departureCities.Concat(arrivalCities).Distinct();

foreach (var c in allCities)
{
    Console.WriteLine($"\t- {c}");
}

//4. Készíts listát az összes különböző indulási városról.

Console.WriteLine("Érkező városok: ");

foreach (var c in arrivalCities)
{
    Console.WriteLine($"\t- {c}");
}

//5. Listázd ki azokat az utakat, amelyek reggel 8:00 előtt indulnak.

var before8am = routes.Where(x => x.DepartureHour < 8).ToList();

Console.WriteLine("Reggel 8 előtt indultak:");

foreach(var r in before8am)
{
    Console.WriteLine($"\t- {r.DepartureCity} → {r.ArrivalCity}, indulás: {r.DepartureHour}:{r.DepartureMinute}, érkezés: {r.ArrivalHour}:{r.ArrivalMinute}, távolság: {r.DistanceKm} km");
}

//6. Számítsd ki minden út esetében a teljes menetidőt percben az indulási és érkezési idő alapján.

var routeLengthInMinutes = routes.Select(x => x.DurrationInMinues).ToList();

Console.WriteLine("Menetidők percben: ");

foreach(var r in routeLengthInMinutes)
{
    Console.WriteLine($"{r} perc");
}

//7. Határozd meg, melyik út rendelkezik a leghosszabb menetidővel, és add meg az adatokat.

var longestRouteInMinutes = routes.FirstOrDefault(x => (x.ArrivalHour * 60 + x.ArrivalMinute) - (x.DepartureHour * 60 + x.DepartureMinute) == routeLengthInMinutes.Max());

Console.WriteLine($"Leghosszabb út (percben): {longestRouteInMinutes.DepartureCity} → {longestRouteInMinutes.ArrivalCity}, indulás: {longestRouteInMinutes.DepartureHour}:{longestRouteInMinutes.DepartureMinute}, érkezés: {longestRouteInMinutes.ArrivalHour}:{longestRouteInMinutes.ArrivalMinute}, távolság: {longestRouteInMinutes.DistanceKm} km");

//8. Számítsd ki minden út esetében az átlagsebességet (km/h) a távolság és a menetidő alapján.

var kmh = routes.Select(x => Math.Round(x.DistanceKm / (double)(x.ArrivalHour - x.DepartureHour))).ToList();

Console.WriteLine("Átlagsebességek: ");

foreach(var s in kmh)
{
    Console.WriteLine($"\t- {s} km/h");
}

// 9. Listázd ki azokat az utakat, ahol az átlagsebesség:
//-nagyobb mint 130 km/h
//- kisebb mint 40 km/h

var over130 = routes.Where(x => Math.Round(x.DistanceKm / (double)(x.ArrivalHour - x.DepartureHour)) > 130).ToList();
var below40 = routes.Where(x => Math.Round(x.DistanceKm / (double)(x.ArrivalHour - x.DepartureHour)) < 40).ToList();

Console.WriteLine("130 km/h felett: ");

foreach(var r in over130)
{
    Console.WriteLine($"\t- {r.DepartureCity} → {r.ArrivalCity}, {Math.Round(r.DistanceKm / (double)(r.ArrivalHour - r.DepartureHour))} km/h");
}

Console.WriteLine("40 km/h alatt: ");

foreach (var r in below40)
{
    Console.WriteLine($"\t- {r.DepartureCity} → {r.ArrivalCity}, {Math.Round(r.DistanceKm / (double)(r.ArrivalHour - r.DepartureHour))} km/h");
}

//10. Csoportosítsd az adatokat érkezési város szerint, és határozd meg:
//-hány út érkezik az adott városba
//- az átlagos távolságot városonként

var arrivalCity = routes.GroupBy(x => x.ArrivalCity).ToDictionary(k => k.Key, v => new List<double>
{
    v.Count(),
    v.Average(x => x.DistanceKm)
});

Console.WriteLine("Adatok csoportosítása érkezési város szerint:");
foreach (var city in arrivalCity)
{
    Console.WriteLine($"{city.Key}, utak száma: {city.Value[0]}, átlagos távolság: {Math.Round(city.Value[1])} km");
}