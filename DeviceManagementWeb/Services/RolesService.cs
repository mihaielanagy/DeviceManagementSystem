using DeviceManagementDB.Repositories;
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

        public List<Role> GetAll()
        {
            return _repository.GetAll();
        }

        public Role GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(Role request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            //var existingRole = _context.Roles.FirstOrDefault(x => x.Name == request.Name);
            //if (existingRole != null)
            //    return 0;

            _repository.Insert(request);
            return request.Id;
        }

        public int Update(Role request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.Name))
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

            var role = _repository.GetById(id);
            if (role == null)
            {
                return 0;
            }

            return _repository.Delete(id);
        }
    }
}
