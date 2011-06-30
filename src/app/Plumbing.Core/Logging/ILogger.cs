using System;

namespace Plumbing.Logging
{
    public interface ILogger
    {
        void Log(LogLevel level, object message);
        void Log(LogLevel level, object message, Exception exception);
        void LogFormat(LogLevel level, string format, params object[] args);
    }

    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }
}