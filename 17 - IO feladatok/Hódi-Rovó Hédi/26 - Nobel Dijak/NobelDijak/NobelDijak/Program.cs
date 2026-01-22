using NobelDijak;
using System.Text;

var fileData = await File.ReadAllLinesAsync("nobel.csv", encoding: Encoding.UTF7);

fileData = fileData.Skip(1).ToArray();

var tudosok = new List<Tudosok>();

foreach (var line in fileData)
{
    var data = line.Split(';');

    if(data.Length == 4)
    {
    tudosok.Add(new Tudosok
    {
        Evszam = int.Parse(data[0]),
        Tipus = data[1],
        Keresztnev = data[2],
        Vezeteknev = data[3]
    });
    }

    else
    {
        tudosok.Add(new Tudosok
        {
            Evszam = int.Parse(data[0]),
            Tipus = data[1],
            Keresztnev = data[2],
            Vezeteknev = ""
        });
    }

}

var arthur = tudosok.FirstOrDefault(x => x.Keresztnev == "Arthur" && x.Vezeteknev == "B. McDonald");

Console.WriteLine($"3. feladat: {arthur?.Tipus}");

var irodalmi = tudosok.FirstOrDefault(x => x.Evszam == 2017 && x.Tipus == "irodalmi");

Console.WriteLine($"4. feladat: {irodalmi?.Keresztnev} {irodalmi?.Vezeteknev}");

List<Tudosok> szervezetek = tudosok.Where(x => x.Vezeteknev == "" && x.Evszam > 1990).ToList();

Console.WriteLine("5. feladat:");
foreach(var szervezet in szervezetek)
{
    Console.WriteLine($"{szervezet.Evszam}: {szervezet.Keresztnev}");
}

List<Tudosok> curiek = tudosok.Where(x => x.Keresztnev == "Curie").ToList();

Console.WriteLine("6. feladat:");
foreach (var curie in curiek)
{
    Console.WriteLine($"{curie.Evszam}: {curie.Keresztnev} {curie.Vezeteknev}({curie.Tipus})");
}

var tipusDarab = tudosok.GroupBy(x => x.Tipus).ToDictionary(k => k.Key, v => v.ToList().Count);

Console.WriteLine("7. feladat:");
foreach( var tipus in tipusDarab)
{
    Console.WriteLine($"{tipus.Key}: {tipus.Value}db");
}

var orvosok = tudosok.Where(x => x.Tipus == "orvosi").ToList();

var stringBuilder = new StringBuilder();
foreach( var orvos in orvosok)
{
    stringBuilder.AppendLine($"{orvos.Evszam};{orvos.Keresztnev} {orvos.Vezeteknev}");
}

await File.WriteAllTextAsync(path: "orvosi.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());