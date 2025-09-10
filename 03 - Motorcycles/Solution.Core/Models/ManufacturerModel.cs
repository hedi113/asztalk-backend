namespace Solution.Core.Models;

public partial class ManufacturerModel : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;


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

}
