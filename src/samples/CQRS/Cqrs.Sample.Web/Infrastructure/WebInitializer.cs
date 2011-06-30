using System;
using System.Collections.Generic;
using Cqrs.Sample.Common.Infrastructure;
using Cqrs.Sample.ViewModels.Infrastructure;
using NServiceBus;

namespace Cqrs.Sample.Web.Infrastructure
{
    public class WebInitializer : ViewModelInitializer
    {
        public WebInitializer(string homeDirectory) : base(homeDirectory, new []{new WebRegistry()})
        {
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            Configure.WithWeb()
                .Log4Net()
                .StructureMapBuilder(Container)
                .XmlSerializer()
                .MsmqSubscriptionStorage()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(false)
                .UnicastBus()
                    .ImpersonateSender(false)
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}