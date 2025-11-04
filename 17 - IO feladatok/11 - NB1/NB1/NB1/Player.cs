namespace NB1;

public class Player
{
    public int Number { get; set; }

    public string? LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool IsHungarian { get; set; }

    public bool IsForeigner { get; set; }

    public int ValueOfPlayer { get; set; }

    public string ClubName { get; set; }

    public string Post { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} - {DateOfBirth}, {Number}, {ValueOfPlayer}, {ClubName}, {Post}";
    }
}
