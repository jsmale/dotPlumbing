using System;
using System.Collections.Generic;
using System.Linq;
using Plumbing.Container;
using Plumbing.DataAccess;
using Plumbing.Logging;

namespace Plumbing.Initialization
{
    public abstract class Initializer : IInitializer 
    {
        public static void Initialize(IInitializer initializer)
        {
            initializer.PreInitialize();
            var loggingDelegate = initializer.ConfigureLogging();
            if (loggingDelegate != null)
            {
                Log.SetLogger(loggingDelegate);
            }
            var createDataContexts = initializer.ConfigureDataContexts();
            IoC.Container = initializer.CreateAndConfigureContainer(loggingDelegate, 
                createDataContexts);
            initializer.PostInitialize();
        }

        public virtual void PreInitialize()
        {            
        }

        public abstract IContainer CreateAndConfigureContainer(GetLoggerDelegate getLoggerDelegate,
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContexts);
        public abstract GetLoggerDelegate ConfigureLogging();

        public IDictionary<Type, Func<IDataAccessContext, IDataContext>> ConfigureDataContexts()
        {
            var dataContextFuncs = CreateDataContextFuncs();
            return dataContextFuncs.Select(x => new KeyValuePair<Type, Func<IDataAccessContext, IDataContext>>
                (x.Key, dataAccessContext =>
                {
                    var dataContext = x.Value();
                    dataAccessContext.SetCurrentDataContext(dataContext, x.Key);
                    return dataContext;
                })).ToDictionary(x => x.Key, x => x.Value);
        }

        public virtual void PostInitialize()
        {
        }

        protected abstract IDictionary<Type, Func<IDataContext>> CreateDataContextFuncs();
    }
}