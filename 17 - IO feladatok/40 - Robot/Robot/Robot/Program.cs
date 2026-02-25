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

int utasitassorSzama = int.Parse(Console.ReadLine());
