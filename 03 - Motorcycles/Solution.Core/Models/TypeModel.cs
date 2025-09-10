namespace Solution.Core.Models;

public partial class TypeModel : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;

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
}
