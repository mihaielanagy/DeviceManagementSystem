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

            return _context.DeviceTypes.Find(id);
        }

        public int Insert(DeviceType request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var existingDt = _context.DeviceTypes.FirstOrDefault(x => x.Name == request.Name);
            if (existingDt != null)
                return 0;

            _context.DeviceTypes.Add(request);
            _context.SaveChanges();
            return request.Id;
        }

        public int Update(DeviceType request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _context.DeviceTypes.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var deviceType = _context.DeviceTypes.Find(id);
            if (deviceType == null)
            {
                return 0;
            }

            _context.DeviceTypes.Remove(deviceType);
            return _context.SaveChanges();
        }
    }
}
