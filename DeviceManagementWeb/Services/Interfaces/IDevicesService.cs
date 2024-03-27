using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDevicesService
    {
        ServiceResponse<List<DeviceDto>> GetAll();
        ServiceResponse<DeviceDto> GetById(int id);
        ServiceResponse<int> Insert(DeviceInsertDto request);
        ServiceResponse<int> Update(DeviceInsertDto request);
        ServiceResponse<int> Delete(int id);
        ServiceResponse<int> UpdateDeviceUser(int deviceId, int? userId);

    }
}
