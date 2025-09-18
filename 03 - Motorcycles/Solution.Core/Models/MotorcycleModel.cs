using CommunityToolkit.Mvvm.ComponentModel;

namespace Solution.Core.Models;

public partial class MotorcycleModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    public string id;

    [ObservableProperty]
    [JsonPropertyName("imageId")]
    public string imageId;

    [ObservableProperty]
    [JsonPropertyName("webContentLink")]
    public string webContentLink;

    [ObservableProperty]
    [JsonPropertyName("manufacturer")]
    public ManufacturerModel manufacturer;

    [ObservableProperty]
    [JsonPropertyName("type")]
    public TypeModel type;

    [ObservableProperty]
    [JsonPropertyName("model")]
    public string model;

    [ObservableProperty]
    [JsonPropertyName("cubic")]
    public int? cubic;

    [ObservableProperty]
    [JsonPropertyName("releaseYear")]
    public int? releaseYear;

    [ObservableProperty]
    [JsonPropertyName("numberOfCylinders")]
    public int? numberOfCylinders;

    public MotorcycleModel() { }

    public MotorcycleModel(MotorcycleEntity entity)
    {
        this.id = entity.PublicId;
        this.imageId = entity.ImageId;
        this.webContentLink = entity.WebContentLink;
        this.manufacturer = new ManufacturerModel(entity.Manufacturer);
        this.type = new TypeModel(entity.Type);
        this.model = entity.Model;
        this.cubic = (int)entity.Cubic;
        this.releaseYear = (int)entity.ReleaseYear;
        this.numberOfCylinders = (int)entity.Cylinders;
    }

    public MotorcycleEntity ToEntity()
    {
        return new MotorcycleEntity
        {
            PublicId = Id,
            ManufacturerId = Manufacturer.Id,
            TypeId = Type.Id,
            ImageId = ImageId,
            WebContentLink = WebContentLink,
            Model = Model,
            Cubic = Cubic.Value,
            ReleaseYear = ReleaseYear.Value,
            Cylinders = NumberOfCylinders.Value
        };
    }

    public void ToEntity(MotorcycleEntity entity)
    {
        entity.PublicId = Id;
        entity.ManufacturerId = Manufacturer.Id;
        entity.TypeId = Type.Id;
        entity.ImageId = ImageId;
        entity.WebContentLink = WebContentLink;
        entity.Model = Model;
        entity.Cubic = Cubic.Value;
        entity.ReleaseYear = ReleaseYear.Value;
        entity.Cylinders = NumberOfCylinders.Value;
    }
}
