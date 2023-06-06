namespace DeviceManagementWeb.DTOs
{
    public class DeviceInsertDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int IdManufacturer { get; set; }

        public int IdDeviceType { get; set; }

        public int IdOsVersion { get; set; }

        public int IdProcessor { get; set; }

        public int IdRamAmount { get; set; }

        public int? IdUser { get; set; } = null;

    }
}
