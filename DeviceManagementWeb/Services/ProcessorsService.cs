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
    }
}
