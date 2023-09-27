namespace DeviceManagementWeb.Services.Interfaces
{
    public interface ILoggingService
    {
        void LogInformation(string message);

        void LogException(Exception exception);
       
    }
}
