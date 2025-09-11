using CommunityToolkit.Mvvm.ComponentModel;

namespace Solution.Core.Models;

public partial class MotorcycleModel : ObservableObject
{
    [ObservableProperty]
    public string id;

    [ObservableProperty]
    public string imageId;

    [ObservableProperty]
    public string webContentLink;

    [ObservableProperty]
    public ManufacturerModel manufacturer;

    [ObservableProperty]
    public TypeModel type;

    [ObservableProperty]
    public string model;

    [ObservableProperty]
    public int? cubic;

    [ObservableProperty]
    public int? releaseYear;

    [ObservableProperty]
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
