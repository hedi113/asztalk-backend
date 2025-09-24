namespace Solution.Core.Models;

public partial class TypeModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    public TypeModel()
    {
        
    }

    public TypeModel(int Id, string Name)
    {
        id = Id;
        name = Name;
    }

    public TypeModel(MotorcycleTypeEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        id = entity.Id;
        name = entity.Name;
    }

    public MotorcycleTypeEntity ToEntity() 
    {
        return new MotorcycleTypeEntity
        {
            Id = Id,
            Name = Name
        };
    }
}
