using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeviceManagementDB.Models;

public partial class Ramamount
{
    public int Id { get; set; }

    public int Amount { get; set; }
    [JsonIgnore]

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
