using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class RamAmountsService : IRamAmountsService
    {
        private readonly DeviceManagementContext _context;
        public RamAmountsService(DeviceManagementContext context)
        {
            _context = context;
        }
        public List<Ramamount> GetAll()
        {
            return _context.Ramamounts.ToList();
        }

        public Ramamount GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Ramamounts.FirstOrDefault(i => i.Id == id);
        }

        public int Insert(Ramamount request)
        {
            if (request.Amount <= 0)
            {
                return 0;
            }

            var existing = _context.Ramamounts.FirstOrDefault(x => x.Amount == request.Amount);
            if (existing != null)
                return 0;

            _context.Ramamounts.Add(request);
            _context.SaveChanges();
            return request.Id;
        }

        public int Update(Ramamount request)
        {
            if (request == null || request.Id <= 0 || request.Amount <=0)
            {
                return 0;
            }

            _context.Ramamounts.Update(request);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }

            var amount = _context.Ramamounts.Find(id);
            if (amount == null)
            {
                return 0;
            }

            _context.Ramamounts.Remove(amount);
            return _context.SaveChanges();
        }
    }
}
