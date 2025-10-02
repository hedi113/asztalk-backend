using konyvek;
using System.Text;

var fileData = await File.ReadAllLinesAsync("adatok.txt", encoding: Encoding.UTF7);

var books = new List<Book>(); 

foreach (var line in fileData)
{
    var data = line.Split('\t');
    books.Add(new Book
    {
        FirstName = data[0],
        LastName = data[1],
        Birthday = DateTime.Parse(data[2]),
        Title = data[3],
        ISBN = data[4],
        Publisher = data[5],
        ReleaseYear = int.Parse(data[6]),
        Price = int.Parse(data[7]),
        Theme = data[8],
        PageNumber = int.Parse(data[9]),
        Honorarium = int.Parse(data[10])
    });
}


//Írjuk ki a képernyőre az össz adatot
Console.WriteLine("feladat 1:");
foreach (var book in books)
{
    Console.WriteLine(book);
}

//Keressük ki az informatika témajú könyveket és mentsük el őket az informatika.txt állömányba

var ITbooks = fileData.Where(x => x.Contains("informatika"));

await File.WriteAllLinesAsync(path: "informatika.txt", encoding: Encoding.UTF8, contents: ITbooks);

//Az 1900.txt állományba mentsük el azokat a könyveket amelyek az 1900-as években íródtak

var booksPublishedIn20thCentury = books.Where(x => x.ReleaseYear >= 1900)
                                       .Where(x => x.ReleaseYear < 2000)
                                       .Select(x => x.ToFullString());

await File.WriteAllLinesAsync(path: "1900.txt", encoding: Encoding.UTF8, contents: booksPublishedIn20thCentury);

Console.ReadKey();