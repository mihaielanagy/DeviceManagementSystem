using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Services
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set;}
        
        public ServiceResponse()  {}
        public ServiceResponse(T? data, bool isSuccess, string? errorMessage = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}
