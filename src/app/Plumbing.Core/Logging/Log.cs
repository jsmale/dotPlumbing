using System;

namespace Plumbing.Logging
{
    public delegate ILogger GetLoggerDelegate(Type type);

    public static class Log
    {
        static GetLoggerDelegate getLogger;
        public static void SetLogger(GetLoggerDelegate getLoggerDelegate)
        {
            getLogger = getLoggerDelegate;
        }

        public static ILogger GetLogger(Type type = null)
        {
            if (getLogger == null)
            {
                return NullLogger.Instance;
            }
            return getLogger(type);
        }
    }
}