using System;
using System.Collections.Generic;

namespace DeviceManagementDB.Models;

public partial class Device
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdManufacturer { get; set; }

    public int IdDeviceType { get; set; }

    public int IdOsversion { get; set; }

    public int IdProcessor { get; set; }

    public int IdRamamount { get; set; }

    public int? IdCurrentUser { get; set; }

    public virtual User? IdCurrentUserNavigation { get; set; }

    public virtual DeviceType IdDeviceTypeNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;

    public virtual OperatingSystemVersion IdOsversionNavigation { get; set; } = null!;

    public virtual Processor IdProcessorNavigation { get; set; } = null!;

    public virtual Ramamount IdRamamountNavigation { get; set; } = null!;
}
