using Lift;
using System.Collections;
using System.Text;

var fileData = await File.ReadAllLinesAsync("igeny.txt", encoding: Encoding.UTF8);

var file = fileData.Skip(3).ToArray();

List<Igeny> igenyek =  new List<Igeny>();

foreach (var item in file)
{
    var data = item.Split(' ');

    igenyek.Add(new Igeny
    {
        IgenylesIdeje = TimeSpan.Parse($"{data[0]}:{data[1]}:{data[2]}"),
        Csapatszam = int.Parse(data[3]),
        InduloSzint = int.Parse(data[4]),
        CelSzint = int.Parse(data[5]),
    });
}

//2. Tudjuk, hogy a megfigyelés kezdetén a lift éppen áll. Kérje be a felhasználótól, hogy melyik szinten áll a lift, és a további részfeladatok megoldásánál ezt vegye figyelembe! Ha a
//beolvasást nem tudja elvégezni, használja az igény.txt fájlban az első igény induló
//szintjét! 

Console.Write("Adja meg, hogy most melyik szinten van a lift: ");
int szint = int.Parse(Console.ReadLine());

//3. Határozza meg, hogy melyik szinten áll majd a lift az utolsó kérés teljesítését követően!
//Írja képernyőre a választ a következőhöz hasonló formában: „A lift a 33. szinten
//áll az utolsó igény teljesítése után.” ! 

int utolsoSzint = igenyek.Last().CelSzint;
Console.WriteLine($"A lift a {utolsoSzint}. szinten áll az utolsó igény teljesítése után.");

//4. Írja a képernyőre, hogy a megfigyelés kezdete és az utolsó igény teljesítése között melyik
//volt a legalacsonyabb és melyik a legmagasabb sorszámú szint, amelyet a lift érintett! 

List<int> szintek = new();

foreach (var igeny in igenyek)
{
    szintek.Add(igeny.InduloSzint);
    szintek.Add(igeny.CelSzint);
}

Console.WriteLine($"A legmagasabb szint, amit a lift érintett: {szintek.Min()}\nA legalacsonyabb szint, amit a lift érintett: {szintek.Max()}");

//5. Határozza meg, hogy hányszor kellett a liftnek felfelé indulnia utassal és hányszor utas
//nélkül! Az eredményt jelenítse meg a képernyőn! 

bool vanEBenneCsapat = true;
int mostaniSzint = 0;
int utassal = 0;
int utasNelkul = 0;

foreach (var igeny in igenyek)
{
    mostaniSzint = igeny.InduloSzint;
    if(igeny.CelSzint > mostaniSzint && vanEBenneCsapat == true)
    {
        utassal++;
    }
    else
    {
        utasNelkul++;
    }
}

Console.WriteLine($"Utassal: {utassal}\nUtas nélkül: {utasNelkul}");

//6.Határozza meg, hogy mely szerelőcsapatok nem vették igénybe a liftet a vizsgált intervallumban! A szerelőcsapatok sorszámát egymástól egy-egy szóközzel elválasztva írja a képernyőre! 

List<int> igenybeVettek = igenyek.Select(x => x.Csapatszam).Distinct().ToList();

List<int> csapatszamok = new();
for (int i = 0; i < 25; i++)
{
    csapatszamok.Add(i);
}

List<int> nemVettekIgenybe = igenybeVettek.Except(csapatszamok).ToList();
Console.WriteLine("Nem vették igénybe a liftet: ");
foreach(int i  in nemVettekIgenybe)
{
    Console.Write($" {i}");
}

//7.Előfordul, hogy egyik vagy másik szerelőcsapat áthágja a szabályokat, és egyik szintről
//gyalog megy a másikra. (Ezt onnan tudhatjuk, hogy más emeleten igényli a liftet, mint
//ahova korábban érkezett.) Generáljon véletlenszerűen egy létező csapatsorszámot! (Ha
//nem jár sikerrel, dolgozzon a 3. csapattal!) Határozza meg, hogy a vizsgált időszak igényei alapján lehet-e egyértelműen bizonyítani, hogy ez a csapat vétett a szabályok ellen!
//Ha igen, akkor adja meg, hogy melyik két szint közötti utat tették meg gyalog, ellenkező
//esetben írja ki a Nem bizonyítható szabálytalanság szöveget! 

Random rnd = new Random();
int vizsgalt = rnd.Next(1, 26);

bool talalt = false;
int a = 0, b = 0;

int? elozoCel = null;

foreach (var i in igenyek)
{
    if (i.Csapatszam == vizsgalt)
    {
        if (elozoCel != null && elozoCel != i.InduloSzint)
        {
            talalt = true;
            a = elozoCel.Value;
            b = i.InduloSzint;
            break;
        }

        elozoCel = i.CelSzint;
    }
}

if (talalt)
{
    Console.WriteLine($"Szabálytalanság: {a} -> {b}");
}
else
{
    Console.WriteLine("Nem bizonyítható szabálytalanság");
}

//8.A munkák elvégzésének adminisztrálásához minden csapatnak egy blokkoló kártyát kell
//használnia. A kártyára a liftben elhelyezett blokkolóóra rögzíti az emeletet, az időpontot.
//Ennek a készüléknek a segítségével kell megadni a munka kódszámát és az adott munkafolyamat sikerességét. A munka kódja 1 és 99 közötti egész szám lehet. A sikerességet a
//„befejezett” és a „befejezetlen” szavakkal lehet jelezni.
//Egy műszaki hiba folytán az előző feladatban vizsgált csapat kártyájára az általunk nyomon követett időszakban nem került bejegyzés. Ezért a csapatfőnöknek a műszak végén
//pótolnia kell a hiányzó adatokat. Az igeny.txt állomány adatait felhasználva írja a képernyőre időrendben, hogy a vizsgált időszakban milyen kérdéseket tett fel az óra, és kérje
//be az adott válaszokat a felhasználótól! A pótlólag feljegyzett adatokat írja a blokkol.txt állományba!

List<string> ki = new List<string>();

foreach (var i in igenyek)
{
    if (i.Csapatszam == vizsgalt)
    {
        Console.WriteLine();
        Console.WriteLine($"Idő: {i.IgenylesIdeje.Hours}:{i.IgenylesIdeje.Minutes}:{i.IgenylesIdeje.Seconds}");
        Console.WriteLine($"Indulási emelet: {i.InduloSzint}");
        Console.WriteLine($"Célemelet: {i.CelSzint}");

        Console.Write("Feladatkód: ");
        string kod = Console.ReadLine();

        Console.Write("Befejezés ideje: ");
        string ido = Console.ReadLine();

        Console.Write("Sikeresség: ");
        string siker = Console.ReadLine();

        ki.Add("Indulási emelet: " + i.InduloSzint);
        ki.Add("Célemelet: " + i.CelSzint);
        ki.Add("Feladatkód: " + kod);
        ki.Add("Befejezés ideje: " + ido);
        ki.Add("Sikeresség: " + siker);
        ki.Add("-----");
    }
}

File.WriteAllLines("blokkol.txt", ki);