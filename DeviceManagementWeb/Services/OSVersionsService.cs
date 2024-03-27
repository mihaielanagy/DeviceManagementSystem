using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class OSVersionsService : IDataService<OsVersionDto>
    {
        private readonly IBaseRepository<OperatingSystemVersion> _repository;
        private readonly IMapper _mapper;

        public OSVersionsService(IBaseRepository<OperatingSystemVersion> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<List<OsVersionDto>> GetAll()
        {
            var osvList = new List<OsVersionDto>();
            var dbList = _repository.GetAll();

            foreach (var osv in dbList)
            {
                osvList.Add(_mapper.Map<OsVersionDto>(osv));
            }

            return new ServiceResponse<List<OsVersionDto>>(osvList, true);
        }

        public ServiceResponse<OsVersionDto> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<OsVersionDto>(null, false, "Invalid id");

            var operatingSystemVersion = _repository.GetById(id);
            var item = _mapper.Map<OsVersionDto>(operatingSystemVersion);
            return new ServiceResponse<OsVersionDto>(item, true);
        }

        public  ServiceResponse<int> Insert(OsVersionDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Name cannot be null");

            if (request.OS == null)
                return new ServiceResponse<int>(0, false, "OS Version not found");

            var osv = new OperatingSystemVersion
            {
                Name = request.Name,
                IdOs = request.OS.Id,
            };

            _repository.Insert(osv);
            return new ServiceResponse<int>(osv.Id, true);            
        }

        public ServiceResponse<int> Update(OsVersionDto request)
        {
            if (request == null)
                return new ServiceResponse<int>(0, false, "Name cannot be null");

            if (request.Id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var osv = _repository.GetById(request.Id);
            osv.Name = request.Name;
            osv.IdOs = request.OS.Id;

            var affectedRows = _repository.Update(osv);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var osv = _repository.GetById(id);
            if (osv == null)
                return new ServiceResponse<int>(0, false, "OS Version not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }

        //private OsVersionDto MapOSVersion(OperatingSystemVersion request)
        //{
        //    var osv = new OsVersionDto
        //    {
        //        Id = request.Id,
        //        Name = request.Name,
        //        OS = _osService.GetById(request.IdOs),
        //    };

        //    return osv;
        //}
    }
}
