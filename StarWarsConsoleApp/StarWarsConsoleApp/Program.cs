
using StarWarsConsoleApp;
using StarWarsConsoleApp.Enums;
using System.ComponentModel;
using System.Text;

var characters = new List<Character>();

var fileData = File.ReadAllLines("star wars.csv").Skip(1);


foreach(var line in fileData)
{
    var data = line.Split(",");

    characters.Add(new Character
    {
        Id = int.Parse(data[0]),
        Name = data[1],
        OrderType = Enum.Parse<OrderTypeEnum>(data[2]),
        Species = data[3],
        Homeworld = data[4],
        Era = Enum.Parse<EraEnum>(data[5]),
        Rank = Enum.Parse<RankEnum>(data[6]),
        LightSaberColor = data[7],
        Master = data[8],
        Apprentice = data[9],
        ForceSpeciality = data[10],
        IsAlive = bool.Parse(data[11]),
    });
}

//2.feladat – Összes karakter száma (2 pont)

Console.WriteLine($"Összes karakter száma: {characters.Count}");

//3.feladat – Jedi és Sith száma (3 pont)

Console.WriteLine($"Jedik száma: {characters.Count(x => x.OrderType == OrderTypeEnum.Jedi)}\nSithek száma: {characters.Count(x => x.OrderType == OrderTypeEnum.Sith)}");

//4. feladat – Piros fénykardosok listája (3 pont) 

var redLightSabers = characters.Where(x => x.LightSaberColor == "Red").Select(x => x.Name).ToList();

StringBuilder stringBuilder = new StringBuilder();

Console.WriteLine("Piros fénykardosok:");
foreach(var c in redLightSabers)
{
    stringBuilder.AppendLine(c);
}

Console.WriteLine($"{stringBuilder.AppendJoin(",", redLightSabers)}");

//5. feladat – Tatooine származású karakterek (3 pont)

var tatooine = characters.Where(x => x.Homeworld == "Tatooine").Select(x => x.Name).ToList();

stringBuilder.Clear();
Console.WriteLine("Tatooine származású karakterek: ");
foreach(var c in tatooine)
{
    stringBuilder.AppendLine(c);
}
Console.WriteLine($"{stringBuilder.AppendJoin(",", tatooine)}");


//6. feladat – Jedi mesterek (3 pont)

var jediMasters = characters.Where(x => x.Rank == RankEnum.Master).Select(x => x.Name).ToList();

stringBuilder.Clear();
Console.WriteLine("Jedi mesterek: ");
foreach(var c in jediMasters)
{
    stringBuilder.AppendLine(c);
}
Console.WriteLine($"{stringBuilder.AppendJoin(",", jediMasters)}");
//7. feladat – Különböző bolygók száma (2 pont)

Console.WriteLine($"Különböző bolygók száma: {characters.DistinctBy(x => x.Homeworld).Count()}");

//8. feladat – Era szerinti csoportosítás (4 pont)

var gorupByEra = characters.GroupBy(x => x.Era).ToDictionary(k => k.Key, v => v.Count());

Console.WriteLine("Era szerinti csoportosítás: ");
foreach(var v in gorupByEra)
{
    Console.WriteLine($"\t- {v.Key}: {v.Value}");
}

//9. feladat – Leggyakoribb fénykard szín (3 pont)

var lightsabersByColor = characters.GroupBy(x => x.LightSaberColor).ToDictionary(k => k.Key, v => v.Count());

Console.WriteLine($"Leggyakoribb fénykard szín: {lightsabersByColor.Max(x => x.Value)}");

//10. feladat – Tanítvánnyal rendelkező karakterek

var withApprentice = characters.Where(x => x.Apprentice != "None").ToDictionary(k => k.Name, v => v.Apprentice);

Console.WriteLine("Tanítvánnyal rendelkező karakterek: ");
foreach(var c in withApprentice)
{
    Console.WriteLine($"\t- {c.Key} → {c.Value}");
}