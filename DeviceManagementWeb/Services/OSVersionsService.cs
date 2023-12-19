using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class OSVersionsService : IDataService<OsVersionDto>
    {
        private readonly IBaseRepository<OperatingSystemVersion> _repository;
        private readonly IBaseRepository<OperatingSystem> _osRepository;

        public OSVersionsService(IBaseRepository<OperatingSystemVersion> repository, IBaseRepository<OperatingSystem> osRepository)
        {
            _repository = repository;
            _osRepository = osRepository;
        }

        public List<OsVersionDto> GetAll()
        {
            var OSVList = new List<OsVersionDto>();
            var dbList = _repository.GetAll();

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

            var operatingSystemVersion = _repository.GetById(id);

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

            _repository.Insert(osv);

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

            var osv = _repository.GetById(request.Id);
            osv.Name = request.Name;
            osv.IdOs = request.OS.Id;

            return _repository.Update(osv);
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                return 0;
            }

            var osv = _repository.GetById(id);
            if (osv == null)
                return 0;

            return _repository.Delete(id);
        }

        private OsVersionDto MapOSVersion(OperatingSystemVersion request)
        {
            var osv = new OsVersionDto
            {
                Id = request.Id,
                Name = request.Name,
                OS = _osRepository.GetById(request.IdOs),
            };

            return osv;
        }
    }
}
