using System.IO;
using Cqrs.Sample.CommandService.Infrastructure;
using NServiceBus;
using Plumbing.Initialization;

namespace Cqrs.Sample.CommandService
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            var initializer = new CommandInitializer(Directory.GetCurrentDirectory());
            Initializer.Initialize(initializer);

            Configure.With()
                .Log4Net()
                .StructureMapBuilder(initializer.Container)
                .XmlSerializer()
                .MsmqSubscriptionStorage()
                .MsmqTransport()
                    .IsTransactional(true)
                .UnicastBus()
                    .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}
