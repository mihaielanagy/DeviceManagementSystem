using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class OperatingSystemsService : IOperatingSystemsService
    {
        private readonly DeviceManagementContext _context;

        public OperatingSystemsService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<OperatingSystem> GetAll()
        {
            return _context.OperatingSystems.ToList();
        }

        public OperatingSystem GetById(int id)
        {
            if (id <= 0)
                return null;

            var operatingSystem = _context.OperatingSystems.FirstOrDefault(i => i.Id == id);

            return operatingSystem;
        }

        public int Insert(OperatingSystem request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _context.OperatingSystems.Add(request);
            _context.SaveChanges();

            return request.Id;
        }

        public int Update(OperatingSystem request)
        {
            if (request == null || request.Id == 0)
            {
                return 0;
            }

            _context.OperatingSystems.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                return 0;
            }

            var OS = _context.OperatingSystems.Find(id);
            if (OS == null)
                return 0;

            _context.OperatingSystems.Remove(OS);

            return _context.SaveChanges();
        }
    }
}
