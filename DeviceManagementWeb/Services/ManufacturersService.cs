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

        public int Insert(Manufacturer request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var existingManufacturer = _context.Manufacturers.FirstOrDefault(x => x.Name == request.Name);
            if (existingManufacturer != null)
                return 0;

            _context.Manufacturers.Add(request);
            _context.SaveChanges();
            return request.Id;
        }

        public int Update(Manufacturer request)
        {
            if(request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _context.Manufacturers.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var manufacturer = _context.Manufacturers.Find(id);
            if(manufacturer == null)
            {
                return 0;
            }

            _context.Manufacturers.Remove(manufacturer);
            return _context.SaveChanges();
        }
    }
}
