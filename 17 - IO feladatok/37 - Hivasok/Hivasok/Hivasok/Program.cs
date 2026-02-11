using Hivasok;
using System.Text;

var file = await File.ReadAllLinesAsync("hivasok.txt");

List<Hivas> hivasok = new List<Hivas>();

foreach (var line in file)
{
    var data = line.Split(' ');

    if (data.Length > 1)
    {
        hivasok.Add(new Hivas
        {
            HivasKezdete = TimeSpan.Parse($"{data[0]}:{data[1]}:{data[2]}"),
            HivasVege = TimeSpan.Parse($"{data[3]}:{data[4]}:{data[5]}"),
        });
    }
    else
    {
        var hivas = hivasok.Last();

        hivas.Telefonszam = data[0];
    }
    
}

//1.Kérjen be a felhasználótól egy telefonszámot! Állapítsa meg a program segítségével,
//hogy a telefonszám mobil-e vagy sem! A megállapítást írja ki a képernyőre! 

Console.Write("Telefonszám: ");
string telefonszam = Console.ReadLine();

if ($"{telefonszam[0]}{telefonszam[1]}" == "39" || $"{telefonszam[0]}{telefonszam[1]}" == "41" || $"{telefonszam[0]}{telefonszam[1]}" == "71")
{
    Console.WriteLine("A megadott telefonszám mobil hívószám.");
}
else
{
    Console.WriteLine("A megadott telefonszám nem mobil hívószám.");

}

//2.Kérjen be továbbá egy hívás kezdeti és hívás vége időpontot óra perc másodperc
//formában! A két időpont alapján határozza meg, hogy a számlázás szempontjából hány
//perces a beszélgetés! A kiszámított időtartamot írja ki a képernyőre!

Console.Write("Adja meg a hívás kezdetének időpontját (ó:p:mp): ");
var kezdet = TimeSpan.Parse(Console.ReadLine());
Console.Write("Adja meg a hívás befejezésének időpontját (ó:p:mp): ");
var vege = TimeSpan.Parse(Console.ReadLine());

var kul = new TimeSpan();

if(vege > kezdet)
{
    kul = vege - kezdet;
}
else { kul = kezdet - vege; }

if(kul.Seconds  > 0)
{
    Console.WriteLine($"A hívás hossza: {kul.TotalMinutes + 1}");
}
else
{
    Console.WriteLine($"A hívás hossza: {kul.TotalMinutes}");
}


//3.Állapítsa meg a hivasok.txt fájlban lévő hívások időpontja alapján, hogy hány
//számlázott percet telefonált a felhasználó hívásonként! A kiszámított számlázott perceket
//írja ki a percek.txt fájlba a következő formában!

var telefonszamok = hivasok.GroupBy(x => x.Telefonszam).ToDictionary(x => x.Key, v => v.ToList());

var stringBuilder = new StringBuilder();
foreach(var felhasznalo in telefonszamok)
{
    double totalCallMinutes = 0;
    foreach(var hivas in felhasznalo.Value)
    {
        totalCallMinutes = hivas.HivasVege.TotalMinutes - hivas.HivasKezdete.TotalMinutes;
    }
    stringBuilder.AppendLine($"{felhasznalo.Key} {totalCallMinutes}");
}

await File.WriteAllTextAsync(contents: stringBuilder.ToString(), path: "percek.txt", encoding: Encoding.UTF8);

//4.Állapítsa meg a hivasok.txt fájl adatai alapján, hogy hány hívás volt csúcsidőben és
//csúcsidőn kívül! Az eredményt jelenítse meg a képernyőn!

var hivasokCsucsidoben = hivasok.Where(x => x.HivasKezdete.Hours > 7 && x.HivasKezdete.Hours < 18).Count();
var hivasokCsucsidonKivul = hivasok.Where(x => x.HivasKezdete.Hours < 7 && x.HivasKezdete.Hours > 18).Count();

Console.WriteLine($"Hívások csúcsidőben: {hivasokCsucsidoben}\nHívások csúcsidőn kívül: {hivasokCsucsidonKivul}");

//5.A hivasok.txt fájlban lévő időpontok alapján határozza meg, hogy hány percet
//beszélt a felhasználó mobil számmal és hány percet vezetékessel! Az eredményt jelenítse
//meg a képernyőn!

