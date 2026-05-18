using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Database.Entities;

public class ChampionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Role { get; set; }

    public string Lane { get; set; }

    public int Difficulity { get; set; }

    public int BlueEssence { get; set; }

    public string DamageType { get; set; }

    public string Description { get; set; }
}
