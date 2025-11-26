using snooker;
using System.Text;

var fileData = await File.ReadAllLinesAsync("snooker.txt", encoding: Encoding.UTF8);

fileData = fileData.Skip(1).ToArray();

var versenyzok = new List<Versenyzo>();

foreach(var line in fileData)
{
    var data = line.Split(";");
    
    versenyzok.Add(new Versenyzo
    {
        Helyezes = data[0],
        Nev = data[1],
        Orszag = data[2],
        Nyeremeny = int.Parse(data[3])
    });
}

// 3.
Console.WriteLine($"3. feladat: A világranglistán {versenyzok.Count} versenyző szerepel.");

// 4.
double avg = Math.Round(versenyzok.Average(x => x.Nyeremeny), 3);

Console.WriteLine($"4. feladat: A versenyzők átlagosan {avg} fontot kerestek.");

// 5.
var legjobbKinaiJatekos = versenyzok.Where(x => x.Orszag == "K�na").MaxBy(x => x.Nyeremeny);

Console.WriteLine($"5. feladat: A legjobban kereső kínai játékos:\n{legjobbKinaiJatekos.ToString()}");

// 6.
bool vaneNorvegjatekos = versenyzok.Any(x => x.Orszag == "Norv�gia");

if(vaneNorvegjatekos)
{
    Console.WriteLine("6. feladat: A versenyzők között van norvég játékos.");
}
else
{
    Console.WriteLine("6. feladat: A versenyzők között nincs norvég játékos.");
}

// 7.
Console.WriteLine("7. feladat: Statisztika");
var jatekosokOrszagonkent = versenyzok.GroupBy(x => x.Orszag).ToDictionary(k => k.Key, v => v.Count());

foreach(var jatekos in  jatekosokOrszagonkent)
{
    if(jatekos.Value > 4)
    {
        Console.WriteLine($"\t{jatekos.Key} - {jatekos.Value} fő");
    }
}