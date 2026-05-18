using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Database.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
