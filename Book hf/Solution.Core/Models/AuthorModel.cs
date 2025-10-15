namespace Solution.Core.Models;

public partial class AuthorModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("birthYear")]
    private int? birthYear;

    public AuthorModel()
    {
        
    }

    public AuthorModel(AuthorEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        id = entity.Id;
        name = entity.Name;
        birthYear = entity.BirthYear;
    }

    public AuthorEntity ToEntity()
    {
        return new AuthorEntity
        {
            Id = Id,
            Name = Name,
            BirthYear = BirthYear.Value,
        };
    }
    public override bool Equals(object? obj)
    {
        return obj is AuthorModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.BirthYear == model.BirthYear;
    }
}
