using System;
using System.Collections.Generic;
using Plumbing.Container;
using Plumbing.DataAccess;
using Plumbing.Logging;

namespace Plumbing.Initialization
{
    public interface IInitializer
    {
        void PreInitialize();
        IContainer CreateAndConfigureContainer(GetLoggerDelegate getLoggerDelegate,
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContexts);
        GetLoggerDelegate ConfigureLogging();
        IDictionary<Type, Func<IDataAccessContext, IDataContext>> ConfigureDataContexts();
        void PostInitialize();
    }
}