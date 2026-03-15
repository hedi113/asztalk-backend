using System.Text;

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

int x = 0, y = 0;
double maxTav = 0;
int lepesIndex = 0;

int lepes = 0;

foreach (char c in utasitas)
{
    lepes++;

    switch (c)
    {
        case 'E': y++; break;
        case 'D': y--; break;
        case 'K': x++; break;
        case 'N': x--; break;
    }

    double tav = Math.Sqrt(x * x + y * y);

    if (tav > maxTav)
    {
        maxTav = tav;
        lepesIndex = lepes;
    }
}

Console.WriteLine($"Visszatérés:");
Console.WriteLine($"{Math.Abs(y)} lépést kell tenni az ED, {Math.Abs(x)} lépést a KN tengely mentén.");

Console.WriteLine($"Legtávolabbi pont: {lepesIndex} {maxTav:F3}");

//3.A robot a mozgáshoz szükséges energiát egy beépített akkuból nyeri. A robot
//1 centiméternyi távolság megtételéhez 1 egység, az irányváltásokhoz és az induláshoz
//2 egység energiát használ. Ennek alapján az EKK utasítássor végrehajtásához 7 egység
//energia szükséges. A szakkörön használt teljesen feltöltött kis kapacitású akkuból 100, a
//nagykapacitásúból 1000 egységnyi energia nyerhető ki. Adja meg azon utasítássorokat,
//amelyek végrehajtásához a teljesen feltöltött kis kapacitású akku is elegendő! Írja a képernyőre egymástól szóközzel elválasztva az utasítássor sorszámát és a szükséges energia
//mennyiségét! Minden érintett utasítássor külön sorba kerüljön! 

Console.WriteLine("\nKis akkuval végrehajtható programok:");

for (int i = 0; i < utasitasok.Count; i++)
{
    string s = utasitasok[i];

    int e = 0;
    char elozo = ' ';

    for (int j = 0; j < s.Length; j++)
    {
        e += 1;

        if (j == 0)
            e += 2;
        else if (s[j] != elozo)
            e += 2;

        elozo = s[j];
    }

    if (e <= 100)
        Console.WriteLine($"{i + 1} {e}");
}

//Gáborék továbbfejlesztették az utasításokat értelmező programot. Az új, jelenleg még
//tesztelés alatt álló változatban a több, változatlan irányban tett elmozdulást helyettesítjük
//az adott irányban tett elmozdulások számával és az irány betűjével. Tehát például a
//DDDKDD utasítássor leírható rövidített 3DK2D formában is. Az önállóan álló utasításnál
//az 1-es számot nem szabad kiírni! Hozza létre az ujprog.txt állományt, amely a
//program.txt állományban foglalt utasítássorozatokat az új formára alakítja úgy, hogy
//az egymást követő azonos utasításokat minden esetben a rövidített alakra cseréli! Az
//ujprog.txt állományba soronként egy utasítássor kerüljön, a sorok ne tartalmazzanak
//szóközt! 

List<string> res = new List<string>();

StringBuilder stringBuilder = new StringBuilder();
foreach (string line in utasitasok)
{
    stringBuilder = new StringBuilder();
    int count = 1;

    for (int i = 1; i <= line.Length; i++)
    {
        if (i < line.Length && line[i] == line[i - 1])
            count++;
        else
        {
            if (count > 1)
                stringBuilder.Append(count);

            stringBuilder.Append(line[i - 1]);
            count = 1;
        }
    }

    res.Add(stringBuilder.ToString());
}

await File.WriteAllLinesAsync("ujprog.txt", res);

//Sajnos a tesztek rámutattak arra, hogy a program új verziója még nem tökéletes, ezért
//vissza kell térni az utasítássorok leírásának régebbi változatához. Mivel a szakkörösök nagyon bíztak az új változatban, ezért néhány utasítássort már csak ennek megfelelően készítettek el. Segítsen ezeket visszaírni az eredeti formára! Az ismétlődések száma legfeljebb 200 lehet! Kérjen be egy új formátumú utasítássort, majd írja a képernyőre régi formában!

Console.WriteLine("Adjon meg egy új formátumú utasítássort: ");
string input = Console.ReadLine();

stringBuilder = new StringBuilder();
int number = 0;

foreach (char c in input)
{
    if (char.IsDigit(c))
    {
        number = number * 10 + (c - '0');
    }
    else
    {
        int ismetles = number == 0 ? 1 : number;

        for (int i = 0; i < ismetles; i++)
            stringBuilder.Append(c);

        number = 0;
    }
}

Console.WriteLine(res.ToString());