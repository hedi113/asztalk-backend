namespace Solution.Core.Models;

public partial class ManufacturerModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    public ManufacturerModel()
    {
        
    }

    public ManufacturerModel(ManufacturerEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        id = entity.Id;
        name = entity.Name;
    }

    public ManufacturerEntity ToEntity()
    {
        return new ManufacturerEntity
        {
            Id = Id,
            Name = Name
        };
    }
    public override bool Equals(object? obj)
    {
        return obj is ManufacturerModel model &&
            this.Id == model.Id &&
            this.Name == model.Name; 
    }
}
