namespace DeviceManagementWeb.DTOs
{
    public class UserInsertDto
    { 
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int IdRole { get; set; }

        public int IdLocation { get; set; }

        public string Email { get; set; } = null!;
        public string Password { get; set; }
    }

}
