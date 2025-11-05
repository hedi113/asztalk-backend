using System.Text;
using System.Text.Unicode;
using System.Text.RegularExpressions;
using Roplabda;

var fileData = await File.ReadAllLinesAsync("adatok.txt", encoding: Encoding.UTF7);

var volleyballPlayers = new List<Volleyball>();

foreach (var line in fileData)
{
    var data = line.Split('\t');
    volleyballPlayers.Add(new Volleyball
    {
        Name = data[0],
        Height = int.Parse(data[1]),
        Post = data[2],
        Nationality = data[3],
        Team = data[4],
        Country = data[5]
    });
}

//- Írjuk ki a képernyőre az össz adatot

foreach (var player in volleyballPlayers)
{
    Console.WriteLine(player.ToString());
}

//- Keressük ki az ütő játékosokat az utok.txt állömányba

var utok = fileData.Where(x => x.Contains("ütõ"));

await File.WriteAllLinesAsync(path: "utok.txt", encoding: Encoding.UTF8, contents: utok);

//- A csapattagok.txt állományba mentsük a csapatokat és a hozzájuk tartozó játékosokat a következő formában:
//Telekom Baku: Yelizaveta Mammadova, Yekaterina Gamova,

var membersByTeam = volleyballPlayers.GroupBy(x => x.Team).ToDictionary(k => k.Key, v => v.ToList());
var stringBuilder = new StringBuilder();
foreach (var team in membersByTeam)
{
    stringBuilder.AppendLine($"{team.Key}:");
    foreach(var member in team.Value)
    {
        stringBuilder.Append($"{member.Name},");
    }
}

await File.WriteAllTextAsync(path: "csapattagok.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());

//- Rendezzük a játékosokat magasság szerint növekvő sorrendbe és a magaslatok.txt állományba mentsük el.
var playersByHeight = volleyballPlayers.OrderBy(x => x.Height).ToList();
stringBuilder.Clear();
foreach(var player in playersByHeight)
{
    stringBuilder.AppendLine($"{player}");
}

await File.WriteAllTextAsync(path: "magaslatok.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());

//- Mutassuk be a nemzetisegek.txt állományba, hogy mely nemzetiségek képviseltetik magukat a röplabdavilágban mint játékosok és milyen számban.

var membersByNationality = volleyballPlayers.GroupBy(x => x.Nationality).ToDictionary(k => k.Key, v => v.ToList());
stringBuilder.Clear();
foreach (var nationality in membersByNationality)
{
    stringBuilder.AppendLine($"{nationality.Key}: {nationality.Value.Count}");
}

await File.WriteAllTextAsync(path: "nemzetisegek.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());

//atlagnalmagasabbak.txt állományba keressük azon játékosok nevét és magasságát akik magasabbak mint az „adatbázisban” szereplő játékosok átlagos magasságánál.

double avgHeight = volleyballPlayers.Average(x => x.Height);
var playersWithAboveAvgHeight = volleyballPlayers.Where(x => x.Height > avgHeight).ToList();
stringBuilder.Clear();
foreach (var player in playersWithAboveAvgHeight)
{
    stringBuilder.AppendLine($"{player.ToString()}");
}

await File.WriteAllTextAsync(path: "atlagnalmagasabbak.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());


//- Állítsa növekvő sorrendbe a posztok szerint a játékosok ösz magasságát

//var sortedMembers = volleyballPlayers.OrderBy(x => x.Height).GroupBy(x => x.Nationality);

var playersByPost = volleyballPlayers.GroupBy(x => x.Post).ToDictionary(k => k.Key, v => v.ToList());
var teamHeight = new Dictionary<string, int>();
foreach (var team in playersByPost)
{
    teamHeight.Add(team.Key, team.Value.Sum(x => x.Height));
}

teamHeight.OrderBy(x => x.Value);


//- Egy szöveges állományba, „alacsonyak.txt” keresse ki a játékosok átlagmagasságától alacsonyabb játékosokat. Az állomány tartalmazza a játékosok nevét,  magasságát és hogy mennyivel alacsonyabbak az átlagnál, 2 tizedes pontossággal.
var playersWithBelowAvgHeight = volleyballPlayers.Where(x => x.Height < avgHeight).ToList();
stringBuilder.Clear();
foreach(var player in playersWithBelowAvgHeight)
{
    stringBuilder.AppendLine($"{player.Name} - {player.Height} - különbség: {Math.Round(avgHeight - player.Height, 2)}");
}

await File.WriteAllTextAsync(path: "alacsonyak.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());
