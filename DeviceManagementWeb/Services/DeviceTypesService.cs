using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class DeviceTypesService : IDataService<DeviceType>
    {
        private readonly IBaseRepository<DeviceType> _repository;
        public DeviceTypesService(IBaseRepository<DeviceType> repository)
        {
            _repository = repository;
        }

        public ServiceResponse<List<DeviceType>> GetAll()
        {
            var types = _repository.GetAll();
            return new ServiceResponse<List<DeviceType>>(types, true);
        }

        public ServiceResponse<DeviceType> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<DeviceType>(null, false, "Invalid id");

            var type = _repository.GetById(id);
            return new ServiceResponse<DeviceType>(type, true);

        }

        public ServiceResponse<int> Insert(DeviceType request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Name cannot be empty");


            var existingDt = _repository.GetById(request.Id);
            if (existingDt != null)
                return new ServiceResponse<int>(0, false, "DeviceType already exists");


            _repository.Insert(request);
            return new ServiceResponse<int>(request.Id, true);
        }

        public ServiceResponse<int> Update(DeviceType request)
        {
            if (request.Id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            if (string.IsNullOrEmpty(request.Name) || request == null)
                return new ServiceResponse<int>(0, false, "Name cannot be empty");

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return new ServiceResponse<int>(0, false, "Device Type not found");

            dbItem.Name = request.Name;
            var affectedRows = _repository.Update(dbItem);

            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid Id");


            var deviceType = _repository.GetById(id);

            if (deviceType == null)
                return new ServiceResponse<int>(0, false, "Device Type not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
