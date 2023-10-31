namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IRolesService
    {
        List<Role> GetAll();
        Role GetById(int id);
        int Insert(Role request);
        int Update(Role request);
        int Delete(int id);
    }
}
