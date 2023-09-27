using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class RolesService : IRolesService
    {
        private readonly DeviceManagementContext _context;
        public RolesService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Roles.FirstOrDefault(i => i.Id == id);
        }
    }
}
