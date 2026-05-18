using WatchesConsoleApp;

var watches = new List<Watch>();

var fileData = File.ReadLines("adatok.csv").Skip(1);

foreach(var line in fileData)
{
    var data = line.Split(',');
    watches.Add(new Watch
    {
        Manufacturer = data[0],
        Model = data[1],
        ReleaseYear = int.Parse(data[2]),
        Type = data[3],
        Movement = data[4],
        WaterResistanceM = int.Parse(data[5]),
        CaseMaterial = data[6],
        Functions = data[7],
        Category = data[8],
    });
}

//2 - Hány karóra található az adatbázisban

Console.WriteLine($"{watches.Count} óra van az adatbázisban.");

//3 - Határozd meg melyik a legrégebbi modell és írd ki a teljes adatait

var oldestModell = watches.MinBy(x => x.ReleaseYear);
Console.WriteLine("Legrégebbi modell:");
Console.WriteLine($"- {oldestModell.Manufacturer}\n- {oldestModell.Model}\n- {oldestModell.ReleaseYear}\n- {oldestModell.Type}\n- {oldestModell.Movement}\n- {oldestModell.WaterResistanceM}\n- {oldestModell.CaseMaterial}\n- {oldestModell.Functions}\n- {oldestModell.Category}");

//4- Listázd ki azokat az órákat amelyek vízállósága legalább 200 méter

var waterResistance200 = watches.Where(x => x.WaterResistanceM >= 200).ToList();

Console.WriteLine("Min. 200m vizállóság:");
foreach(var w in waterResistance200)
{
    Console.WriteLine($"- {w.Manufacturer} {w.Model}");
}

//5 - Kérj be a felhasználótól egy kulcsszót (pl. "GPS" vagy "kronográf"), majd: 
//  listázd ki azokat az órákat, amelyek functions mezője tartalmazza ezt

Console.Write("Kulcsszó: ");
var keyWord = Console.ReadLine();
var matches = new List<Watch>();

foreach(var w in watches)
{
    if(w.Functions.Contains(keyWord))
    {
        matches.Add(w);
    }
}

if(matches.Count > 0)
{
    Console.WriteLine("Ilyen funkcióval rendelkező órák: ");
    foreach(var w in matches)
    {
        Console.WriteLine($"- {w.Manufacturer} {w.Model}");
    }
}
else
{
    Console.WriteLine("Nincs ilyen funkcióval rendelkező óra az adatbázisban!");
}

//6 - Számold ki az órák átlagos vízállóságát

Console.WriteLine($"Az órák átlagos vízállósága: {watches.Average(x => x.WaterResistanceM)} m");

//7 - Csoportosítsd az órákat category szerint, majd írd ki:
//	luxury(15 db):
//	 -Rolex Submariner
//     - Omega Speedmaster
//    midrange(20 db):

var watchesByCategory = watches.GroupBy(x => x.Category).ToDictionary(k => k.Key, v => v.ToList());
Console.WriteLine("Csoportosítás kategória szerint:");
foreach(var w in watchesByCategory)
{
    Console.WriteLine($"{w.Key} ({w.Value.Count} db):");
    foreach(var v in w.Value) 
    {
        Console.WriteLine($"\t- {v.Manufacturer} {v.Model}");
    }
}

//8 - Gyártónként írd ki:
//	-hány modelljük van
//    -átlagos vízállóság
//	Példa:
//	Rolex:
//      Modellek száma: 3
//      Átlag vízállóság: 166.6 m

var watchesByManufacturer = watches.GroupBy(x => x.Manufacturer).ToDictionary(k => k.Key, v => v.ToList());

Console.WriteLine("Csoportosítás gyártó szerint:");
foreach(var w in watchesByManufacturer)
{
    Console.WriteLine($"{w.Key}:\n \tModellek száma: {w.Value.Count}\n\tÁtlag vízállóság: {w.Value.Average(x => x.WaterResistanceM)} m");
}

//9 - Határozd meg melyik movement típus fordul elő a legtöbbször

var movementTypes = watches.GroupBy(x => x.Movement).ToDictionary(k => k.Key, v => v.Count());

var maxMovement = movementTypes.MaxBy(x => x.Value);

Console.WriteLine($"A legtöbbször előforduló óraszerkezet: {maxMovement.Key}");

//10 - Listázd ki azokat az órákat:
//	 -amelyek luxury kategóriába tartozna és vízállóságuk ≥ 100 m
//	Majd:
//	-csoportosítsd őket gyártó szerint
//    - rendezd gyártón belül év szerint növekvően
//	- formázottan írd ki:	
//		Omega:
//-1957 Speedmaster Professional

//-1993 Seamaster Diver 300M

//		Rolex:
//		  -1953 Submariner
//          - 1963 Daytona

var luxuryWatches = watches.Where(x => x.WaterResistanceM >= 100 && x.Category == "luxury").GroupBy(x => x.Manufacturer).ToDictionary(k => k.Key, v => v.ToList().OrderBy(x => x.ReleaseYear));

foreach(var w in luxuryWatches)
{
    Console.WriteLine($"{w.Key}:");
    foreach (var v in w.Value)
    {
        Console.WriteLine($"\t- {v.ReleaseYear} {v.Model}");
    }    
}