﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeviceManagementDB.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
