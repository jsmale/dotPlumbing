using System;
using System.Collections.Generic;
using Plumbing.Container;
using Plumbing.DataAccess;
using Plumbing.Extensions;
using Plumbing.Logging;
using StructureMap.Configuration.DSL;

namespace Plumbing.StructureMap
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry(GetLoggerDelegate getLoggerDelegate,
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContexts, 
            params Registry[] registries)
        {
            foreach (var registry in registries)
            {
                IncludeRegistry(registry);
            }

            For<IContainer>().Use(x => new StructureMapContainer(
                x.GetInstance<global::StructureMap.IContainer>()));

            if (getLoggerDelegate != null)
            {
                For<ILogger>().Use(y => getLoggerDelegate(y.ParentType));
            }
            if (createDataContexts != null)
            {
                For<IDataAccessContext>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x => new DataAccessContext());

                createDataContexts.Each(create => 
                    For(create.Key).HybridHttpOrThreadLocalScoped()
                        .Use(x => create.Value(x.GetInstance<IDataAccessContext>())));
            }

            Scan(a =>
            {
                a.AssemblyContainingType<IContainer>();
                a.WithDefaultConventions();
            });
        }
    }
}