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

    
    public ManufacturerModel(uint id, string name)
    {
        id = id;
        name = name;
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

}
