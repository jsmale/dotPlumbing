using System;
using log4net;
using Plumbing.Logging;

namespace Plumbing.Log4net
{
    public class Logger : ILogger
    {
        readonly ILog log;

        public Logger(ILog log)
        {
            this.log = log;
        }

        public void Log(LogLevel level, object message)
        {
            switch(level)
            {
                case LogLevel.Debug:
                    log.Debug(message);
                    return;

                case LogLevel.Info:
                    log.Info(message);
                    return;

                case LogLevel.Warn:
                    log.Warn(message);
                    return;

                case LogLevel.Error:
                    log.Error(message);
                    return;

                case LogLevel.Fatal:
                    log.Fatal(message);
                    return;
            }
        }

        public void Log(LogLevel level, object message, Exception exception)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    log.Debug(message, exception);
                    return;

                case LogLevel.Info:
                    log.Info(message, exception);
                    return;

                case LogLevel.Warn:
                    log.Warn(message, exception);
                    return;

                case LogLevel.Error:
                    log.Error(message, exception);
                    return;

                case LogLevel.Fatal:
                    log.Fatal(message, exception);
                    return;
            }
        }

        public void LogFormat(LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    log.DebugFormat(format, args);
                    return;

                case LogLevel.Info:
                    log.InfoFormat(format, args);
                    return;

                case LogLevel.Warn:
                    log.WarnFormat(format, args);
                    return;

                case LogLevel.Error:
                    log.ErrorFormat(format, args);
                    return;

                case LogLevel.Fatal:
                    log.FatalFormat(format, args);
                    return;
            }
        }
    }
}