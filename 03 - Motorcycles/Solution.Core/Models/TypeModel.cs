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

    public override bool Equals(object? obj)
    {
        return obj is TypeModel model &&
            this.Id == model.Id &&
            this.Name == model.Name;
    }
}
