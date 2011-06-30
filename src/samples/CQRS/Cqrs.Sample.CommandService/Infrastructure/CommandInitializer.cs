using System;
using System.Collections.Generic;
using Cqrs.Sample.Common.Infrastructure;
using EventStore;
using EventStore.Dispatcher;
using NServiceBus;
using System.Linq;
using Plumbing.DataAccess;

namespace Cqrs.Sample.CommandService.Infrastructure
{
    public class CommandInitializer : CommonInitializer
    {
        IStoreEvents eventStore;

        public CommandInitializer(string homeDirectory) : base(homeDirectory, new CommandRegistry())
        {
        }

        protected override IDictionary<Type, Func<IDataContext>> CreateDataContextFuncs()
        {
            return new Dictionary<Type, Func<IDataContext>>();
        }

        public override void PreInitialize()
        {
            base.PreInitialize();
            eventStore = Wireup.Init()
                .UsingSqlPersistence("CqrsSampleEventStore")
                    .InitializeDatabaseSchema()
                .UsingJsonSerialization()
                .UsingAsynchronousDispatcher()
                    .PublishTo(new DelegateMessagePublisher(DispatchCommit))
                    .HandleExceptionsWith(DispatchErrorHandler)
                .Build();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            Container.Configure(x => x.For<IStoreEvents>().Use(eventStore));
        }

        void DispatchCommit(Commit commit)
        {
            var events = commit.Events.Select(x => x.Body as IMessage).ToArray();
            var bus = Container.GetInstance<IBus>();
            bus.Publish(events);
        }

        static void DispatchErrorHandler(Commit commit, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}