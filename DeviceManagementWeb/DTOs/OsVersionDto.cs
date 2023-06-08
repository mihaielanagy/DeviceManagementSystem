namespace DeviceManagementWeb.DTOs
{
    public class OsVersionDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DeviceManagementDB.Models.OperatingSystem OS { get; set; }
    }
}
