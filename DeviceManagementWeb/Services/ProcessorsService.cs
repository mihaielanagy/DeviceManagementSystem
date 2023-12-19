using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Services.Interfaces;
namespace DeviceManagementWeb.Services
{
    public class ProcessorsService : IDataService<Processor>
    {
        private readonly IBaseRepository<Processor> _repository;
        public ProcessorsService(IBaseRepository<Processor> repository)
        {
            _repository = repository;
        }
        public List<Processor> GetAll()
        {
            return _repository.GetAll();
        }

        public Processor GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(Processor request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            //var existingProcessor = _context.Processors.FirstOrDefault(x => x.Name == request.Name);
            //if (existingProcessor != null)
            //    return 0;

            _repository.Insert(request);

            return request.Id;
        }

        public int Update(Processor request)
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

            var processor = _repository.GetById(id);
            if (processor == null)
            {
                return 0;
            }

            return _repository.Delete(id);
        }
    }
}
