using eu_tagallamok;
using System.Linq;
using System.Text;

var fileData = await File.ReadAllLinesAsync("EUcsatlakozas.txt", encoding: Encoding.UTF8);

var tagallamok = new List<Tagallam>();

foreach (var line in fileData)
{
    var data = line.Split(';');
    tagallamok.Add(new Tagallam
    {
        Nev = data[0],
        CsatlakozasDatuma = data[1]
    });
}
    // 3.
    Console.WriteLine($"3. feladat: EU tagállamainak száma: {tagallamok.Count}");

    // 4.
    int? tagallamok2007 = tagallamok.Where(x => x.CsatlakozasDatuma.Contains("2007")).Count();

    Console.WriteLine($"4. feladat: 2007-ben {tagallamok2007} ország csatlakozott.");

    // 5.
    string MoCsatDatum = tagallamok.Where(x => x.Nev == "Magyarorsz�g").First().CsatlakozasDatuma;
    Console.WriteLine($"5. feladat: Magyarország csatlakozásának dátuma: {MoCsatDatum}");

    // 6.
    List<string> csatlakozasok = [.. tagallamok.Select(x => x.CsatlakozasDatuma)];

    foreach(string datum in csatlakozasok )
    {
        var csatDatum = datum.Split(".");
        if (csatDatum[1].Contains("05") == true)
        {
            Console.WriteLine("6. feladat: Májusban volt csatlakozás!");
            break;
        }
        else
        {
            continue;
        }
    
     Console.WriteLine("6. feladat: Májusban nem volt csatlakozás!");
    }


// 7.
List<DateTime> rendezettCsatlakozasok = new();

    foreach(string datum in csatlakozasok)
    {
        rendezettCsatlakozasok.Add(DateTime.Parse(datum));
    }

    DateTime maxDatum = rendezettCsatlakozasok.OrderDescending().First();

    string nev = tagallamok.Where(x => DateTime.Parse(x.CsatlakozasDatuma).Year == maxDatum.Year).First().Nev;

    Console.WriteLine($"7. feladat: A legutoljára csatlakozott ország: {nev}");

    // 8.
    Console.WriteLine("8. feladat: Statisztika");
    var orszagokPerTagallam = tagallamok.GroupBy(x => DateTime.Parse(x.CsatlakozasDatuma).Year).ToDictionary(k => k.Key, v => v.Count());
    foreach(var ev in orszagokPerTagallam)
    {
        Console.WriteLine($"{ev.Key} - {ev.Value}");
    }
