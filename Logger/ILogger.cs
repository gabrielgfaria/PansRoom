namespace Logger
{
    public interface ILogger
    {
        void SetLogMessage(string message);
        void Log();
    }
}