using NB1;
using System.Text;

var fileData = await File.ReadAllLinesAsync("adatok.txt", encoding: Encoding.UTF7);

var soccerPlayers = new List<Player>();

foreach (var line in fileData)
{
    var data = line.Split('\t');
    if (data.Length < 9 )
    {
        soccerPlayers.Add(new Player
        {
            ClubName = data[0],
            Number = int.Parse(data[1]),
            LastName = null,
            FirstName = data[2],
            DateOfBirth = DateTime.Parse(data[3]),
            IsHungarian = (int.Parse(data[4]) == -1) ? true : false,
            IsForeigner = (int.Parse(data[5]) == -1) ? true : false,
            ValueOfPlayer = int.Parse(data[6]),
            Post = data[7],
        });
    }
    else
    {
        soccerPlayers.Add(new Player
        {
            ClubName = data[0],
            Number = int.Parse(data[1]),
            LastName = data[2],
            FirstName = data[3],
            DateOfBirth = DateTime.Parse(data[4]),
            IsHungarian = (int.Parse(data[5]) == -1) ? true : false,
            IsForeigner = (int.Parse(data[6]) == -1) ? true : false,
            ValueOfPlayer = int.Parse(data[7]),
            Post = data[8],
        });
    }
}

//a) A kapusokon kívül mindenkit mezőnyjátékosnak tekintünk. Keresse ki a legidősebb mezőnyjátékos vezeték- és utónevét, valamint születési dátumát! (Feltételezheti, hogy csak egy ilyen játékos van.)

void OldestPlayer() { 
    var notGoalies = soccerPlayers.Where(x => x.Post != "kapus").ToList();
    var oldestPlayer = notGoalies.OrderBy(x => x.DateOfBirth).FirstOrDefault();
    Console.WriteLine($"{oldestPlayer.FirstName} {oldestPlayer.LastName} - {oldestPlayer.DateOfBirth}");
}

//b) Határozza meg hány magyar, külföldi és kettős állampolgárságú játékos van! 
void Nationality() {
    var hungarians = soccerPlayers.Count(x => x.IsHungarian == true && x.IsForeigner == false);
    var foreigners = soccerPlayers.Count(x => x.IsForeigner == true && x.IsHungarian == false);
    var both = soccerPlayers.Count(x => x.IsHungarian == true && x.IsForeigner == true);
    Console.WriteLine($"Magyarok: {hungarians}\nKülföldiek: {foreigners}\nKettős álammpolgárságúak: {both}");
}

//c) Határozza meg játékosok összértékét csapatonként és írja ki a képernyőre! A csapatok neve és a játékosainak összértéke jelenjen meg!

void ValueOfTeam()
{
    var playersByTeam = soccerPlayers.GroupBy(x => x.ClubName).ToDictionary(k => k.Key, v => v.ToList());
    var teamWorth = new Dictionary<string, int>();
    foreach(var team in playersByTeam)
    {
        teamWorth.Add(team.Key, team.Value.Sum(x => x.ValueOfPlayer));
    }

    foreach(var value in teamWorth)
    {
        Console.WriteLine($"{value.Key} - {value.Value}");
    }
}

//d) Keresse ki, hogy mely csapatoknál mely posztokon van csupán egy szerződtetett játékos! Írja ki a csapat nevet és a posztot amire csak egy játékost szerződtettek!

void OnePersonPerPost()
{
    var playersByTeam = soccerPlayers.GroupBy(x => x.ClubName).ToDictionary(k => k.Key, v => v.ToList());
    foreach(var team in playersByTeam)
    {
        var listOfPostsByTeam = team.Value.Select(x => x.Post).ToList();
        var numberOfPostsByTeam = team.Value.GroupBy(x => x.Post).ToDictionary(k => k.Key, v => v.ToList());
        foreach (var post in numberOfPostsByTeam)
        {
            if (post.Value.Count == 1)
            {
                Console.WriteLine($"{team.Key} - {post.Key}");
            }
        }
    }
}

//e) Keressük ki azon játékosokat, akiknek az értékük nem haladja meg a játékosok értékének átlag értékét.

void BelowAvg()
{
    double avgValue = soccerPlayers.Average(x => x.ValueOfPlayer);
    var playersBelowAvg = soccerPlayers.Where(x => x.ValueOfPlayer < avgValue).ToList();
    
    foreach(var player in playersBelowAvg)
    {
        Console.WriteLine(player.ToString());
    }
}

//f) Írja ki azon játékosok nevét, születési dátumát és csapataik nevét, akik 18 és 21 év közt vannak és magyar állampolgárok. Ha nincs ilyen, akkor megfelelő üzenettel helyettesítse a kimenetet.
void SelectedSoccerPlayers()
{
    var selectedSoccerPlayers = soccerPlayers.Where(x => DateTime.UtcNow.Year - x.DateOfBirth.Year > 18 && DateTime.UtcNow.Year - x.DateOfBirth.Year < 21 && x.IsHungarian == true);
    if(selectedSoccerPlayers.Count() == 0)
    {
        Console.WriteLine("Nincs ilyen játékos!");
    }
    else
    {
        foreach(var player in selectedSoccerPlayers)
        { 
            Console.WriteLine(player.ToString()); 
        }
    }
}

//g) A „hazai.txt” illetve a „legios.txt” állományokba keresse ki a magyar, illetve a külföldi állampolgárságú játékosokat csapatonként. A szöveges állományoknak tartalmazniuk kell a csapat nevét majd alatta felsorolva a játékosok teljes nevét, poszt nevet és értéküket.

async void HungarianVSForeignerInTxt()
{
    var hungariansByTeam = soccerPlayers.Where(x => x.IsHungarian == true && x.IsForeigner == false).GroupBy(x => x.ClubName).ToDictionary(k => k.Key, v => v.ToList());
    var foreignersByTeam = soccerPlayers.Where(x => x.IsHungarian == false && x.IsForeigner == true).GroupBy(x => x.ClubName).ToDictionary(k => k.Key, v => v.ToList());
    
    var stringBuilder = new StringBuilder();
    foreach(var team in hungariansByTeam)
    {
        stringBuilder.AppendLine($"{team.Key}\n");
        foreach(var hungarian in team.Value)
        {
            stringBuilder.AppendLine($"\t- {hungarian.FirstName} {hungarian.LastName}, {hungarian.Post}, {hungarian.ValueOfPlayer}");
        }
    }
    await File.WriteAllTextAsync(path: "hazai.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());

    stringBuilder = new StringBuilder();
    foreach (var team in foreignersByTeam)
    {
        stringBuilder.AppendLine($"{team.Key}\n");
        foreach (var foreigner in team.Value)
        {
            stringBuilder.AppendLine($"\t- {foreigner.FirstName} {foreigner.LastName}, {foreigner.Post}, {foreigner.ValueOfPlayer}");
        }
    }
    await File.WriteAllTextAsync(path: "legios.txt", encoding: Encoding.UTF8, contents: stringBuilder.ToString());


    try
    {
        var psiHazai = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "hazai.txt",
            UseShellExecute = true
        };
        var psiLegios = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "legios.txt",
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psiHazai);
        System.Diagnostics.Process.Start(psiLegios);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Nem sikerült megnyitni a fájlokat: {ex.Message}");
    }
}
var menuItems = new (string Title, Action? Action, Func<Task>? AsyncAction)[]
{
    ("Legidősebb mezőnyjátékos", () => OldestPlayer(), null),
    ("Magyar / külföldi / kettős állampolgárságú játékosok száma", () => Nationality(), null),
    ("Csapatonkénti összérték", () => ValueOfTeam(), null),
    ("Poszt, ahol csak egy játékos van", () => OnePersonPerPost(), null),
    ("Átlagnál kisebb értékű játékosok", () => BelowAvg(), null),
    ("18–21 éves magyar játékosok", () => SelectedSoccerPlayers(), null),
    ("Magyar és légiós játékosok fájlba", null, async () => HungarianVSForeignerInTxt()),
    ("Kilépés", null, null)
};

int selectedIndex = 0;

while (true)
{
    Console.Clear();
    Console.WriteLine("  __  __                  \r\n |  \\/  |                 \r\n | \\  / | ___ _ __  _   _ \r\n | |\\/| |/ _ \\ '_ \\| | | |\r\n | |  | |  __/ | | | |_| |\r\n |_|  |_|\\___|_| |_|\\__,_|\r\n                          \r\n                          ");
    for (int i = 0; i < menuItems.Length; i++)
    {
        if (i == selectedIndex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"> {menuItems[i].Title}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"  {menuItems[i].Title}");
        }
    }

    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.UpArrow)
    {
        selectedIndex = (selectedIndex == 0) ? menuItems.Length - 1 : selectedIndex - 1;
    }
    else if (key == ConsoleKey.DownArrow)
    {
        selectedIndex = (selectedIndex + 1) % menuItems.Length;
    }
    else if (key == ConsoleKey.Enter)
    {
        if (menuItems[selectedIndex].Title == "Kilépés")
        {
            Console.Clear();
            Console.WriteLine("Kilépés...");
            break;
        }

        Console.Clear();
        if (menuItems[selectedIndex].Action != null)
            menuItems[selectedIndex].Action();
        else if (menuItems[selectedIndex].AsyncAction != null)
            await menuItems[selectedIndex].AsyncAction();

        Console.WriteLine("Nyomjon meg egy gombot a folytatáshoz...");
        Console.ReadKey(true);
    }
}