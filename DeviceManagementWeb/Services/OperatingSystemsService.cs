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

        public List<OperatingSystem> GetAll()
        {
            return _repository.GetAll();
        }

        public OperatingSystem GetById(int id)
        {
            if (id <= 0)
                return null;


            return _repository.GetById(id);
        }

        public int Insert(OperatingSystem request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _repository.Insert(request);
            return request.Id;
        }

        public int Update(OperatingSystem request)
        {
            if (request == null || request.Id == 0)
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
            if (id < 1)
            {
                return 0;
            }

            var OS = _repository.GetById(id);
            if (OS == null)
                return 0;
                        
            return _repository.Delete(id);
        }
    }
}
