using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plumbing.Container;
using Plumbing.DataAccess;
using Plumbing.Initialization;
using Plumbing.Logging;
using Plumbing.StructureMap;
using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.Common.Infrastructure
{
    public abstract class CommonInitializer : Initializer
    {
        readonly string homeDirectory;
        readonly Registry[] additionalRegistries;
        StructureMapContainer structureMapContainer;

        protected CommonInitializer(string homeDirectory, params Registry[] additionalRegistries)
        {
            this.homeDirectory = homeDirectory;
            this.additionalRegistries = additionalRegistries;
        }

        public override IContainer CreateAndConfigureContainer(GetLoggerDelegate getLoggerDelegate, 
            IDictionary<Type, Func<IDataAccessContext, IDataContext>> createDataContexts)
        {
            structureMapContainer = Configure.CreateAndConfigureContainer(getLoggerDelegate,
                createDataContexts, container => container.Configure(c =>
                    c.SetAllProperties(s => s.TypeMatches(type => container.Model.HasImplementationsFor(type)))),
                additionalRegistries.Concat(new[] { new CommonRegistry() }).ToArray());
            return structureMapContainer;
        }

        public override GetLoggerDelegate ConfigureLogging()
        {
            return Plumbing.Log4net.Configure.ConfigureLogging(
                new FileInfo(Path.Combine(homeDirectory, "logging.config")));
        }

        public StructureMap.IContainer Container
        {
            get { return structureMapContainer.InnerContainer; }
        }
    }
}