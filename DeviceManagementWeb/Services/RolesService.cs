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

        public int Insert(Role request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var existingRole = _context.Roles.FirstOrDefault(x => x.Name == request.Name);
            if (existingRole != null)
                return 0;

            _context.Roles.Add(request);
            _context.SaveChanges();
            return request.Id;
        }

        public int Update(Role request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _context.Roles.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return 0;
            }

            _context.Roles.Remove(role);
            return _context.SaveChanges();
        }
    }
}
