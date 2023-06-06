using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeviceManagementDB.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int IdRole { get; set; }

    public int IdLocation { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual Location IdLocationNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
