using System;

namespace Plumbing.Logging
{
    public class NullLogger : ILogger 
    {
        private NullLogger() {}
        static ILogger instance = new NullLogger();
        public static ILogger Instance
        {
            get { return instance; }
        }

        public void Log(LogLevel level, object message)
        {
        }
        public void Log(LogLevel level, object message, Exception exception)
        {
        }
        public void LogFormat(LogLevel level, string format, params object[] args)
        {
        }
    }
}