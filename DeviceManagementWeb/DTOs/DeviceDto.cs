namespace DeviceManagementWeb.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Manufacturer Manufacturer { get; set; }

        public DeviceType DeviceType { get; set; }

        public OperatingSystemVersion OsVersion { get; set; }

        public Processor Processor { get; set; }

        public Ramamount RamAmount { get; set; }

        public UserDto User { get; set; }

    }
}
