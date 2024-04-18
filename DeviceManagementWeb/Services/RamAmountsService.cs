using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class RamAmountsService : IDataService<Ramamount>
    {
        private readonly IBaseRepository<Ramamount> _repository;
        public RamAmountsService(IBaseRepository<Ramamount> repository)
        {
            _repository = repository;
        }
        public ServiceResponse<List<Ramamount>> GetAll()
        {
            var list = _repository.GetAll();
            return new ServiceResponse<List<Ramamount>>(list, true);
        }

        public ServiceResponse<Ramamount> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<Ramamount>(null, false, "Id not found");

            var item = _repository.GetById(id);
            return new ServiceResponse<Ramamount>(item, true);
        }

        public ServiceResponse<int> Insert(Ramamount request)
        {
            if (request.Amount <= 0)
                return new ServiceResponse<int>(0, false, "Amount cannot be less than 0");

            //var existing = _context.Ramamounts.FirstOrDefault(x => x.Amount == request.Amount);
            //if (existing != null)
            //    return 0;

            _repository.Insert(request);
            return new ServiceResponse<int>(request.Id, true);
        }

        public ServiceResponse<int> Update(Ramamount request)
        {
            if (request == null || request.Amount <= 0)
                return new ServiceResponse<int>(0, false, "Amount cannot be null or less than 0");

            if (request.Id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
               return new ServiceResponse<int>(0, false, "Id not found");

            var affectedRows = _repository.Update(dbItem);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var amount = _repository.GetById(id);
            if (amount == null)
                return new ServiceResponse<int>(0, false, "Id not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
