using StarTrekConsoleApp;
using System.Globalization;

var spaceships = new List<Spaceship>();

var f = await File.ReadAllLinesAsync("star_trek_ships.csv");

var file = f.Skip(1);

foreach(var line in file)
{
    var data = line.Split(',');

    var spaceship = new Spaceship
    {
        Name = data[0],
        Class = data[1],
        RaceFaction = data[2],
        Length = int.Parse(data[3]),
        Crew = int.Parse(data[4]),
        MaxWarp = double.Parse(data[5], CultureInfo.GetCultureInfo("en-EN")),
        Armament = data[6],
        ShieldType = data[7],
        HullMaterial = data[8],
        Role = int.Parse(data[9])
    };

    spaceships.Add(spaceship);
}

//2. Hajók számának meghatározása 

Console.WriteLine($"Hajók száma: {spaceships.Count}");

//3. Legénység összlétszáma 

Console.WriteLine($"Összes legénység: {spaceships.Sum(x => x.Crew)}");

//4. Legnagyobb hajó keresése

var max = spaceships.Max(x => x.Length);

Console.WriteLine($"A legnagyobb hajó: {spaceships.FirstOrDefault(x => x.Length == max).Name}");

//5. Hajók száma frakciónként

var hajoszamFrakcionkent = spaceships.GroupBy(x => x.RaceFaction).ToDictionary(k =>  k.Key, v => v.Count());

foreach(var hajo in hajoszamFrakcionkent)
{
    Console.WriteLine($"\t{hajo.Key}: {hajo.Value}");
}

//6. Warp 9 feletti hajók listázása

var hajok9WarpFelett = spaceships.Where(x => x.MaxWarp > 9).ToList();

Console.WriteLine($"Hajók 9 warp felett: ");
foreach(var hajo in hajok9WarpFelett)
{
    Console.WriteLine($"\t{hajo.Name}: {hajo.MaxWarp}");
}

//7. Szerepkör (role) szerinti csoportosítás

var szerepkoronkent = spaceships.GroupBy(x => x.Role).ToDictionary(k => k.Key, v => v.Count());

Console.WriteLine("Hajók szerepkörönként:");
foreach(var szerep in szerepkoronkent)
{
    Console.WriteLine($"\t{(ShipRoleEnum)szerep.Key}: {szerep.Value}");
}

//8. Átlagos hajóhossz kiszámítása

Console.WriteLine($"Átlag hajóhossz: {spaceships.Average(x => x.Length)}");

//9. Legnagyobb legénységű hajó frakciónként

var legnagyobbLegenyseguHajok = spaceships.GroupBy(x => x.RaceFaction).ToDictionary(k => k.Key, v => v.MaxBy(x => x.Crew));

Console.WriteLine("Legnagyobb legénységű hajók frakciónként:");
foreach(var hajo in legnagyobbLegenyseguHajok)
{
    Console.WriteLine($"\t{hajo.Key}: {hajo.Value}");
}

//10. Top 5 leggyorsabb hajó

var top5Leggyorsabb = spaceships.OrderByDescending(x => x.MaxWarp).Take(5).ToList();
int sorszam = 0;
foreach (var hajo in top5Leggyorsabb)
{
    sorszam++;
    Console.WriteLine($"{sorszam}. {hajo.Name} - Warp {hajo.MaxWarp}");
}