using DeviceManagementDB.Repositories;
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
        public List<Ramamount> GetAll()
        {
            return _repository.GetAll();
        }

        public Ramamount GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(Ramamount request)
        {
            if (request.Amount <= 0)
            {
                return 0;
            }

            //var existing = _context.Ramamounts.FirstOrDefault(x => x.Amount == request.Amount);
            //if (existing != null)
            //    return 0;

            _repository.Insert(request);

            return request.Id;
        }

        public int Update(Ramamount request)
        {
            if (request == null || request.Id <= 0 || request.Amount <=0)
            {
                return 0;
            }

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return 0;

            return _repository.Update(dbItem);
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var amount = _repository.GetById(id);
            if (amount == null)
            {
                return 0;
            }

            return _repository.Delete(id);
        }
    }
}
