using System;
using System.Collections.Generic;
using System.Text;

namespace HeroWars.Database.Entities;

public class Hero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Strength { get; set; }


}
