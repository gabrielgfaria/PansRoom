using System;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : ILogger
    {
        public Logger()
        {
        }

        public static string LogMessage { get; private set; }

        public void SetLogMessage(string message) => LogMessage = message;

        public void Log()
        {
            Console.WriteLine(LogMessage);
            Task.WaitAll(Task.Delay(3000));
        }
    }
}
