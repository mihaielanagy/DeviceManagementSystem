using System;
using System.Collections.Generic;

namespace DeviceManagementDB.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public int IdCity { get; set; }

    public virtual City IdCityNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
