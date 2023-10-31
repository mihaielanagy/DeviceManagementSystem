using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class OSVersionsService : IOsVersionService
    {
        private readonly DeviceManagementContext _context;

        public OSVersionsService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<OsVersionDto> GetAll()
        {
            var OSVList = new List<OsVersionDto>();
            var dbList = _context.OperatingSystemVersions.ToList();

            foreach (var OSV in dbList)
            {
                OSVList.Add(MapOSVersion(OSV));
            }


            return OSVList;
        }

        public OsVersionDto GetById(int id)
        {
            if (id <= 0)
                return null;

            var operatingSystemVersion = _context.OperatingSystemVersions.Find(id);

            return MapOSVersion(operatingSystemVersion);
        }

        public int Insert(OsVersionDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            if (request.OS == null)
            {
                return 0;
            }

            var osv = new OperatingSystemVersion
            {
                Name = request.Name,
                IdOs = request.OS.Id,
            };

            _context.OperatingSystemVersions.Add(osv);
            _context.SaveChanges();

            return osv.Id;
        }

        public int Update(OsVersionDto request)
        {
            if (request == null)
            {
                return 0;
            }

            if (request.Id <= 0)
            {
                return 0;
            }

            var osv = _context.OperatingSystemVersions.Find(request.Id);
            osv.Name = request.Name;
            osv.IdOs = request.OS.Id;

            _context.OperatingSystemVersions.Update(osv);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                return 0;
            }

            var osv = _context.OperatingSystemVersions.Find(id);
            if (osv == null)
                return 0;

            _context.OperatingSystemVersions.Remove(osv);

            return _context.SaveChanges();
        }

        private OsVersionDto MapOSVersion(OperatingSystemVersion request)
        {
            var osv = new OsVersionDto
            {
                Id = request.Id,
                Name = request.Name,
                OS = _context.OperatingSystems.Find(request.IdOs),
            };

            return osv;
        }
    }
}
