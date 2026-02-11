using System.Linq;
using System.Text;
using Tarsalgo;

var file = await File.ReadAllLinesAsync("ajto.txt");

List<Forgalom> beKilepesek = new List<Forgalom>();

foreach (var line in file)
{
    var data = line.Split(' ');

    beKilepesek.Add(new Forgalom
    {
        Idopont = TimeSpan.Parse($"{data[0]}:{data[1]}"),
        Azonosito = int.Parse(data[2]),
        KiBemenetel = data[3]
    });
}

//2.Írja a képernyőre annak a személynek az azonosítóját, aki a vizsgált időszakon belül először
//lépett be az ajtón, és azét, aki utoljára távozott a megfigyelési időszakban! 

var eloszorLepettBe = beKilepesek.First(x => x.KiBemenetel == "be").Azonosito;
var utoljaraMentKi = beKilepesek.Last(x => x.KiBemenetel == "ki").Azonosito;

Console.WriteLine($"Az első belépő: {eloszorLepettBe}\nAz utolsó kilépő: {utoljaraMentKi}");

//3.Határozza meg a fájlban szereplő személyek közül, ki hányszor haladt át a társalgó ajtaján!
//A meghatározott értékeket azonosító szerint növekvő sorrendben írja az athaladas.txt
//fájlba! Soronként egy személy azonosítója, és tőle egy szóközzel elválasztva az áthaladások
//száma szerepeljen! 

var athaladasokSzemelyenkent =  beKilepesek.GroupBy(x => x.Azonosito).ToDictionary(x => x.Key, v => v.ToList());

var stringBuilder = new StringBuilder();

foreach(var szemely in athaladasokSzemelyenkent)
{
    int athaladt = 0;
    foreach(var athaladas in szemely.Value)
    {
        if(athaladas.KiBemenetel == "be")
        {
            athaladt++; 
        }
    }
    stringBuilder.AppendLine($"{szemely.Key} {athaladt}");
}

await File.WriteAllTextAsync(contents: stringBuilder.ToString(), path: "athaladas.txt", encoding: Encoding.UTF8);

//5.Írja a képernyőre azon személyek azonosítóját, akik a vizsgált időszak végén a társalgóban
//tartózkodtak!

List<int> nemMentekKi = new List<int>();

foreach (var szemely in athaladasokSzemelyenkent)
{
    int kiHaladasokSzama = 0;
    int bemenetelekSzama = 0;
    foreach (var athaladas in szemely.Value)
    {
        if(athaladas.KiBemenetel == "be")
        {
            bemenetelekSzama++;
        }
        else
        {
            kiHaladasokSzama++;
        }
    }
    if(bemenetelekSzama > kiHaladasokSzama)
    {
        nemMentekKi.Add(beKilepesek.FirstOrDefault(x => x.Azonosito == szemely.Key).Azonosito);
    }
}


Console.WriteLine($"A végén a társalgóban voltak: ", string.Join(" ", nemMentekKi));


//6.Hányan voltak legtöbben egyszerre a társalgóban? Írjon a képernyőre egy olyan időpontot
//(óra:perc), amikor a legtöbben voltak bent! 

int max = 0;
int egyszerre = 0;
TimeSpan idopont = new TimeSpan();

foreach(var athaladas in beKilepesek)
{
    if(athaladas.KiBemenetel == "be")
    {
        egyszerre++; 
    }
    if (athaladas.KiBemenetel == "ki")
    {
        egyszerre--;
    }
    if(egyszerre > max)
    {
        max = egyszerre;
        idopont = athaladas.Idopont;
    }
}

Console.WriteLine($"Például {idopont.Hours}:{idopont.Minutes}-kor voltak a legtöbben a társalgóban.");

//7.Kérje be a felhasználótól egy személy azonosítóját! A további feladatok megoldásánál ezt
//használja fel! 

Console.Write("Adja meg a személy azonosítóját! ");
int azonosito = int.Parse(Console.ReadLine());

//8.Írja a képernyőre, hogy a beolvasott azonosítóhoz tartozó személy mettől meddig
//tartózkodott a társalgóban! 

var kiBemenetelekAzonositoval = beKilepesek.GroupBy(x => x.Azonosito).ToDictionary(x => x.Key, v => v.Select(x => x.Idopont).ToList());

var keresett = kiBemenetelekAzonositoval.Where(x => x.Key == azonosito).ToDictionary();

var idopontok = new List<TimeSpan>();
foreach(var k in keresett.Values)
{
    foreach(var v in k)
    {
        idopontok.Add(v);
    }
}

for (int i = 0; i < idopontok.Count; i += 2)
{
    string first = $"{idopontok[i].Hours}:{idopontok[i].Minutes}-";
    string second = (i + 1 < idopontok.Count) ? $"{idopontok[i + 1].Hours}:{idopontok[i + 1].Minutes}" : "";
    Console.WriteLine($"{first}{second}");
}

//9.Határozza meg, hogy a megfigyelt időszakban a beolvasott azonosítójú személy összesen
//hány percet töltött a társalgóban! Az előző feladatban példaként szereplő 22-es személy 5
//alkalommal járt bent, a megfigyelés végén még bent volt. Róla azt tudjuk, hogy 18 percet
//töltött bent a megfigyelés végéig. A 39-es személy 6 alkalommal járt bent, a vizsgált időszak
//végén nem tartózkodott a helyiségben. Róla azt tudjuk, hogy 39 percet töltött ott. Írja ki,
//hogy a beolvasott azonosítójú személy mennyi időt volt a társalgóban, és a megfigyelési
//időszak végén bent volt-e még! 

double osszesPerc = 0;
bool bentVoltE = true;
for (int i = 0; i < idopontok.Count; i += 2)
{
    double first = idopontok[i].TotalMinutes;
    double second = (i + 1 < idopontok.Count) ? idopontok[i + 1].TotalMinutes : first;
    if (i + 1 > idopontok.Count)
    {
        bentVoltE = false;
    }
    osszesPerc += second - first;
}
if (bentVoltE)
{
    Console.WriteLine($"A(z) {azonosito}. személy összesen {osszesPerc} percet volt bent, a megfigyelés végén a társalgóban volt.");
}
else
{
    Console.WriteLine($"A(z) {azonosito}. személy összesen {osszesPerc} percet volt bent, a megfigyelés végén nem volt a társalgóban.");
}

Console.ReadKey();