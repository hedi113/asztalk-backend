using HeroWars.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HeroWars.Services.Models;

public class HeroModel : UpdateHeroModel
{
    public int Id { get; set; }
}
