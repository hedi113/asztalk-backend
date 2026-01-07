using LabdarugoVB;
using System.Text;

var fileData = await File.ReadAllLinesAsync("vb2018.txt", encoding: Encoding.UTF7);

fileData = fileData.Skip(1).ToArray();

var stadionok = new List<VB>();

foreach (var line in fileData)
{
    var data = line.Split(';');

    stadionok.Add(new VB
    {
        Varos = data[0],
        Nev1 = data[1],
        Nev2 = data[2],
        FeroHely = int.Parse(data[3])
    });
}
    Console.WriteLine($"3. feladat: Stadionok száma: {stadionok.Count}");

    var legkisebbStadion = stadionok.MinBy(x => x.FeroHely);

    Console.WriteLine($"4. feladat: A legkevesebb férőhely: {legkisebbStadion.ToString()}");

    var atlagFerohely = stadionok.Average(x => x.FeroHely);

    Console.WriteLine($"5. feladat: Átlagos férőhelyszám: {atlagFerohely}");

    int stadionokSzama = 0;
    foreach( var stadion in stadionok )
    {
        if (stadion.Nev2 != "n.a")
        {
            stadionokSzama++;
        }
    }

    Console.WriteLine($"6. feladat: Két néven is ismert stadionok száma: {stadionokSzama}");

    string szoveg = "";
    while(szoveg.Length < 3)
    {
        Console.Write("Kérem a város nevét: ");
        szoveg = Console.ReadLine();
    }
        bool vanE = stadionok.Any(x => x.Varos == szoveg.ToLower());
        if( vanE )
        {
            Console.WriteLine("8. feladat: A megadott város VB helyszín.");
        }
        else
        {
            Console.WriteLine("8. feladat: A megadott város nem VB helyszín.");
        }

    int kulonbozoekSzama = stadionok.GroupBy(x => x.Varos).Count();

    Console.WriteLine($"9. feladat: {kulonbozoekSzama} különböző városban voltak mérkőzések.");

Console.ReadKey();