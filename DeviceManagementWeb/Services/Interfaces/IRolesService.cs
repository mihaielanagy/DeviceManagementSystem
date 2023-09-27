namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IRolesService
    {
        List<Role> GetAll();
        Role GetById(int id);
    }
}
