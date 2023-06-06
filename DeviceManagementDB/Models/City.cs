global using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;


namespace DeviceManagementDB.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdCountry { get; set; }

    public virtual Country IdCountryNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
