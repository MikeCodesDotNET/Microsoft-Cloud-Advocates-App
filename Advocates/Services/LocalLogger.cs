using System.Diagnostics;
using Prism.Logging;

namespace Advocates.Services
{
    public class LocalLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Debug.WriteLine($"{category} - {priority}: {message}");
        }
    }
}
