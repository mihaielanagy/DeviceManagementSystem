using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeviceManagementDB.Models;

public partial class Processor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
