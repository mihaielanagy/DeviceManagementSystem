using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeviceManagementDB.Models;

public partial class OperatingSystem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<OperatingSystemVersion> OperatingSystemVersions { get; set; } = new List<OperatingSystemVersion>();
}
