using System;
using System.Collections.Generic;
using Plumbing.DataAccess;
using Plumbing.Logging;
using StructureMap.Configuration.DSL;

namespace Plumbing.StructureMap
{
    public static class Configure
    {
        public static StructureMapContainer CreateAndConfigureContainer(GetLoggerDelegate getLoggerDelegate,
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContext, 
            params Registry[] registries)
        {
            return CreateAndConfigureContainer(getLoggerDelegate, createDataContext, null, registries);
        }

        public static StructureMapContainer CreateAndConfigureContainer(GetLoggerDelegate getLoggerDelegate,
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContext, 
            Action<global::StructureMap.IContainer> containerAction, 
            params Registry[] registries)
        {
            var registry = new DefaultRegistry(getLoggerDelegate, createDataContext, registries);
            var container = new global::StructureMap.Container(registry);
            if (containerAction != null) containerAction(container);
            return new StructureMapContainer(container);
        }
    }
}