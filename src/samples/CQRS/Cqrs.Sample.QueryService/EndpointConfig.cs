using System.IO;
using Cqrs.Sample.QueryService.Infrastructure;
using NServiceBus;
using Plumbing.Initialization;

namespace Cqrs.Sample.QueryService
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var initializer = new QueryServiceInitializer(Directory.GetCurrentDirectory());
            Initializer.Initialize(initializer);

            Configure.With()
                .Log4Net()
                .StructureMapBuilder(initializer.Container)
                .XmlSerializer()
                .MsmqTransport()
                .UnicastBus()
                    .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}
