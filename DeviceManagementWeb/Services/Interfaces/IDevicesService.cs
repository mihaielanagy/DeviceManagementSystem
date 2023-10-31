using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDevicesService
    {
        List<DeviceDto> GetAll();
        DeviceDto GetById(int id);
        int Insert(DeviceInsertDto request);
        int Update(DeviceInsertDto request);
        int Delete(int id);
        int UpdateDeviceUser(int deviceId, int? userId);

    }
}
