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
        public ServiceResponse<List<Manufacturer>> GetAll()
        {
            var list = _repository.GetAll();
            return new ServiceResponse<List<Manufacturer>>(list, true);
        }


        public ServiceResponse<Manufacturer> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<Manufacturer>(null, false, "Invalid Id");

            var mnf = _repository.GetById(id);
            return new ServiceResponse<Manufacturer>(mnf, true);
        }

        public ServiceResponse<int> Insert(Manufacturer request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Invalid Id");

            //var existingManufacturer = _context.Manufacturers.FirstOrDefault(x => x.Name == request.Name);
            // if (existingManufacturer != null)
            //return 0;

            _repository.Insert(request);
            return new ServiceResponse<int>(request.Id, true);
        }

        public ServiceResponse<int> Update(Manufacturer request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Invalid Id");

            Manufacturer manufacturer = _repository.GetById(request.Id);
            if (manufacturer == null)
                return new ServiceResponse<int>(0, false, "Manufacturer not found");

            manufacturer.Name = request.Name;
            var affectedRows = _repository.Update(manufacturer);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid Id");

            var manufacturer = _repository.GetById(id);
            if (manufacturer == null)
                return new ServiceResponse<int>(0, false, "Manufacturer not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
