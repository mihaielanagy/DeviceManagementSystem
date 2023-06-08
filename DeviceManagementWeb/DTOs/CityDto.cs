namespace DeviceManagementWeb.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Country Country { get; set; }
    }
}
