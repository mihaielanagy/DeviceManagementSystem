using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class OperatingSystemsService : IDataService<OperatingSystem>
    {
        private readonly IBaseRepository<OperatingSystem> _repository;

        public OperatingSystemsService(IBaseRepository<OperatingSystem> repository)
        {
            _repository = repository;
        }

        public ServiceResponse<List<OperatingSystem>> GetAll()
        {
            var list = _repository.GetAll();
            return new ServiceResponse<List<OperatingSystem>>(list, true);
        }

        public ServiceResponse<OperatingSystem> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<OperatingSystem>(null, false, "Invalid id");

            var os = _repository.GetById(id);
            return new ServiceResponse<OperatingSystem>(os, true);
        }

        public ServiceResponse<int> Insert(OperatingSystem request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Name cannot be empty");

            _repository.Insert(request);
            return new ServiceResponse<int>(request.Id, true);
        }

        public ServiceResponse<int> Update(OperatingSystem request)
        {
            if (request.Id == 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            if (request == null)
                return new ServiceResponse<int>(0, false, "Name cannot be empty");

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return new ServiceResponse<int>(0, false, "Operating System not found");

            dbItem.Name = request.Name;
            var affectedRows = _repository.Update(dbItem);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var OS = _repository.GetById(id);
            if (OS == null)
                return new ServiceResponse<int>(0, false, "Operating System not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
