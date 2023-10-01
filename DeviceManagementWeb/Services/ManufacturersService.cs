using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class ManufacturersService: IManufacturersService
    {
        private readonly DeviceManagementContext _context;
        public ManufacturersService(DeviceManagementContext context)
        {
            _context = context;
        }
        public List<Manufacturer> GetAll()
        {
            return _context.Manufacturers.ToList();
        }

        public Manufacturer GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Manufacturers.FirstOrDefault(i => i.Id == id);
        }
    }
}
