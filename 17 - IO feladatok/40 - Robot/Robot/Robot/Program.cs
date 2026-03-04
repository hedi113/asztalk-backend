var file = await File.ReadAllLinesAsync("program.txt");

List<string> utasitasok = new();

foreach (var line in file)
{
    utasitasok.Add(line);
}

//2.Kérje be egy utasítássor számát, majd írja a képernyőre, hogy:
//a.Egyszerűsíthető - e az utasítássorozat! Az egyszerűsíthető, illetve nem egyszerűsíthető választ írja a képernyőre! (Egy utasítássort egyszerűsíthetőnek nevezünk, ha
//van benne két szomszédos, ellentétes irányt kifejező utasításpár, hiszen ezek a párok
//elhagyhatók. Ilyen ellentétes utasításpár az ED, DE, KN, NK.)
//b. Az utasítássor végrehajtását követően legkevesebb mennyi E vagy D és K vagy N utasítással lehetne a robotot a kiindulási pontba visszajuttatni! A választ a következő
//formában jelenítse meg: 3 lépést kell tenni az ED, 4 lépést a KN tengely
//mentén.
//c. Annak végrehajtása során hányadik lépést követően került (légvonalban) legtávolabb a
//robot a kiindulási ponttól és mekkora volt ez a távolság! A távolságot a lépés sorszámát követően 3 tizedes pontossággal írja a képernyőre!

Console.Write("Adja meg az utasítás sorszámát: ");
int utasitassorSzama = int.Parse(Console.ReadLine());

string utasitas = utasitasok[utasitassorSzama];

if((utasitas.Contains("ED") && utasitas.Contains("DE")) || (utasitas.Contains("KN") && utasitas.Contains("NK")))
{
    Console.WriteLine("Az utasítássor egyszerűsíthető.");
}
else
{
    Console.WriteLine("Az utasítássor nem egyszerűsíthető.");
}

GetPosition(utasitas);

Dictionary<string, int> GetPosition(string utasitas)
{
    Dictionary<string, int> position = new();

    position.Add("E", 0);
    position.Add("D", 0);
    position.Add("K", 0);
    position.Add("N", 0);

    foreach(char c in utasitas)
    {
        int value = 0;
        if(c == char.Parse("E"))
        {
            position.TryGetValue("E", out value);
            position["E"] = ++value;
        }
        if (c == char.Parse("D"))
        {
            position.TryGetValue("D", out value);
            position["D"] = ++value;
        }
        if (c == char.Parse("K"))
        {
            position.TryGetValue("K", out value);
            position["K"] = ++value;
        }
        if (c == char.Parse("N"))
        {
            position.TryGetValue("N", out value);
            position["N"] = ++value;
        }
    }

    return position;
}

int ekPositionDiff = GetPosition(utasitas)["E"] - GetPosition(utasitas)["D"];
int knPositionDiff = GetPosition(utasitas)["K"] - GetPosition(utasitas)["N"];

Console.WriteLine($"{Math.Abs(ekPositionDiff)} lépést kell tenni az ED, {Math.Abs(knPositionDiff)} lépést a KN tengely\r\nmentén.");