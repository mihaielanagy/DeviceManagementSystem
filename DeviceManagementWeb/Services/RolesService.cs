using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class RolesService : IDataService<Role>
    {
        private readonly IBaseRepository<Role> _repository;
        public RolesService(IBaseRepository<Role> repository)
        {
            _repository = repository;
        }

        public ServiceResponse<List<Role>> GetAll()
        {
            var list = _repository.GetAll();
            return new ServiceResponse<List<Role>>(list, true);
        }

        public ServiceResponse<Role> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<Role>(null, false, "Invalid id");

            var item = _repository.GetById(id);
            return new ServiceResponse<Role>(item, true);
        }

        public ServiceResponse<int> Insert(Role request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Role name cannot be empty");

            //var existingRole = _context.Roles.FirstOrDefault(x => x.Name == request.Name);
            //if (existingRole != null)
            //    return 0;

            _repository.Insert(request);
            return new ServiceResponse<int>(request.Id, true);           
        }

        public ServiceResponse<int> Update(Role request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
                return new ServiceResponse<int>(0, false, "Role name cannot be empty");

            var dbItem = _repository.GetById(request.Id);
            if (dbItem == null)
                return new ServiceResponse<int>(0, false, "Id not found");

            var affectedRows = _repository.Update(dbItem);
            return new ServiceResponse<int>(affectedRows,true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var role = _repository.GetById(id);
            if (role == null)
                return new ServiceResponse<int>(0, false, "Id not found");

            var affectedRows = _repository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);            
        }
    }
}
