using Lift;
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
}