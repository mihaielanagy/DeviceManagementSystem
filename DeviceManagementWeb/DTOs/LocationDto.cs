namespace DeviceManagementWeb.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }

        public string Address { get; set; } = null!;

        public CityDto City { get; set; }
    }
}
