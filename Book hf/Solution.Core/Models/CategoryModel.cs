namespace Solution.Core.Models;

public partial class CategoryModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    public CategoryModel()
    {
        
    }

    public CategoryModel(CategoryEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        id = entity.Id;
        name = entity.Name;
    }

    public CategoryEntity ToEntity() 
    {
        return new CategoryEntity
        {
            Id = Id,
            Name = Name
        };
    }

    public override bool Equals(object? obj)
    {
        return obj is CategoryModel model &&
            this.Id == model.Id &&
            this.Name == model.Name;
    }
}
