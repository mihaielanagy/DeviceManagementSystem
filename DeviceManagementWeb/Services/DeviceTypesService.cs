using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class DeviceTypesService : IDeviceTypesService
    {
        private readonly DeviceManagementContext _context;

        public DeviceTypesService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<DeviceType> GetAll()
        {
            return _context.DeviceTypes.ToList();
        }

        public DeviceType GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.DeviceTypes.FirstOrDefault(i => i.Id == id);
        }
    }
}
