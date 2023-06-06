namespace DeviceManagementWeb.DTOs
{
    public class UserDto
    { 
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Role Role { get; set; }

        public Location Location { get; set; }

        public string Email { get; set; } = null!;
    }

}
