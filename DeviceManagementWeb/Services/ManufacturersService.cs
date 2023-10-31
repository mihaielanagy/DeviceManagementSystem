using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class ManufacturersService : IDataService<Manufacturer>
    {
        private readonly IBaseRepository<Manufacturer> _repository;
        public ManufacturersService(IBaseRepository<Manufacturer> repository)
        {
            _repository = repository;
        }
        public List<Manufacturer> GetAll()
        {
            return _repository.GetAll();
        }


        public Manufacturer GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(Manufacturer request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            //var existingManufacturer = _context.Manufacturers.FirstOrDefault(x => x.Name == request.Name);
            // if (existingManufacturer != null)
            //return 0;

            _repository.Insert(request);
            return request.Id;
        }

        public int Update(Manufacturer request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            Manufacturer manufacturer = _repository.GetById(request.Id);
            if (manufacturer == null)
                return 0;

            manufacturer.Name = request.Name;

            return _repository.Update(manufacturer);
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var manufacturer = _repository.GetById(id);
            if (manufacturer == null)
            {
                return 0;
            }

            return _repository.Delete(id);
        }
    }
}
