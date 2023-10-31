using DeviceManagementWeb.Services.Interfaces;
namespace DeviceManagementWeb.Services
{
    public class ProcessorsService: IProcessorsService
    {
        DeviceManagementContext _context;
        public ProcessorsService(DeviceManagementContext context)
        {
            _context = context;
        }
        public List<Processor> GetAll()
        {
            return _context.Processors.ToList();
        }

        public Processor GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Processors.FirstOrDefault(i => i.Id == id);
        }

        public int Insert(Processor request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            var existingProcessor = _context.Processors.FirstOrDefault(x => x.Name == request.Name);
            if (existingProcessor != null)
                return 0;

            _context.Processors.Add(request);
            _context.SaveChanges();
            return request.Id;
        }

        public int Update(Processor request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            _context.Processors.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var processor = _context.Processors.Find(id);
            if (processor == null)
            {
                return 0;
            }

            _context.Processors.Remove(processor);
            return _context.SaveChanges();
        }
    }
}
