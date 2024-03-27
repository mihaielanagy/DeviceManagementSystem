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
        public ServiceResponse<List<Processor>> GetAll()
        {
            var list = _repository.GetAll();
            return new ServiceResponse<List<Processor>>(list, true);
        }

        public ServiceResponse<Processor> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<Processor>(null, false, "Invalid Id");

            var processor = _repository.GetById(id);
            return new ServiceResponse<Processor>(processor, true);

        }

        public ServiceResponse<int> Insert(Processor request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Processor name cannot be null.");

            //var existingProcessor = _context.Processors.FirstOrDefault(x => x.Name == request.Name);
            //if (existingProcessor != null)
            //    return 0;

            _repository.Insert(request);

            return new ServiceResponse<int>(request.Id, true);
        }

        public ServiceResponse<int> Update(Processor request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Processor name cannot be null.");

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return new ServiceResponse<int>(0, false, "Id not found.");


            dbItem.Name = request.Name;
            var affectedRows = _repository.Update(dbItem);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id.");

            var processor = _repository.GetById(id);
            if (processor == null)
                return new ServiceResponse<int>(0, false, "Id not found.");
            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
