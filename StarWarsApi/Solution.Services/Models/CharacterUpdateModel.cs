using Solution.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Services.Models;

public class CharacterUpdateModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public OrderTypeEnum OrderType { get; set; }

    public string Species { get; set; }
    public string Homeworld { get; set; }
public EraEnum Era { get; set; }

    public RankEnum Rank { get; set; }
    public string LightSaberColor { get; set; }

    public string Master { get; set; }


    public string Apprentice { get; set; }
    public string ForceSpeciality { get; set; }

    public bool IsAlive { get; set; }
}
