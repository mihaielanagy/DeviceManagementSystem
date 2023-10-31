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

        public List<DeviceType> GetAll()
        {
            return _repository.GetAll();
        }

        public DeviceType GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(DeviceType request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var existingDt = _repository.GetById(request.Id);
            if (existingDt != null)
                return 0;

            _repository.Insert(request);
            return request.Id;
        }

        public int Update(DeviceType request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return 0;

            dbItem.Name = request.Name;

            return _repository.Update(dbItem);
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var deviceType = _repository.GetById(id);

            if (deviceType == null)
            {
                return 0;
            }

            return _repository.Delete(id);
        }
    }
}
