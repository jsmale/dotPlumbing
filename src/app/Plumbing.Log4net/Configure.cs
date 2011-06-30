using System.IO;
using log4net;
using log4net.Config;
using Plumbing.Logging;

namespace Plumbing.Log4net
{
    public static class Configure
    {
        public static GetLoggerDelegate ConfigureLogging(FileInfo configurationFile)
        {
            XmlConfigurator.ConfigureAndWatch(configurationFile);
            return type => new Logger(LogManager.GetLogger(type ?? typeof(Logger)));
        }
    }
}