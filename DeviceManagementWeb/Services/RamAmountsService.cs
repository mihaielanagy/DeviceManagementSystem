using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class RamAmountsService : IRamAmountsService
    {
        DeviceManagementContext _context;
        public RamAmountsService(DeviceManagementContext context)
        {
            _context = context;
        }
        public List<Ramamount> GetAll()
        {
            return _context.Ramamounts.ToList();
        }

        public Ramamount GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Ramamounts.FirstOrDefault(i => i.Id == id);
        }
    }
}
