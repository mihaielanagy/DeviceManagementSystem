using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class LoggingService : ILoggingService
    {
        public void LogInformation(string message)
        {
            //Will log info
        }

        public void LogException(Exception exception)
        {
            //Will log exception
        }

    }
}
