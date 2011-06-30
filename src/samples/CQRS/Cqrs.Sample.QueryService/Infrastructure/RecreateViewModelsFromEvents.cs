using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cqrs.Sample.Common.Infrastructure;
using Cqrs.Sample.Events;
using Cqrs.Sample.ViewModels.Infrastructure;
using EventStore.Persistence.SqlPersistence;
using EventStore.Persistence.SqlPersistence.SqlDialects;
using EventStore.Serialization;
using NServiceBus;
using System.Linq;
using Plumbing.Container;

namespace Cqrs.Sample.QueryService.Infrastructure
{
    public class RecreateViewModelsFromEvents : DropCreateDatabaseIfModelChanges<ViewModelContext>
    {
        public bool NeedsData { get; private set; }

        protected override void Seed(ViewModelContext context)
        {
            NeedsData = true;
        }

        public void UpdateModelsFromEvents()
        {
            var handlerType = typeof (IHandleMessages<>);
            var eventType = typeof (IEvent);
            var events = eventType.Assembly.GetTypes().Where(eventType.IsAssignableFrom);
            var handlers = new Dictionary<Type, object[]>();
            var assembly = GetType().Assembly;
            foreach (var @event in events)
            {
                var eventHandlerType = handlerType.MakeGenericType(@event);
                var eventHandlerTypes = assembly.GetTypes().Where(eventHandlerType.IsAssignableFrom);
                var implementations = new List<object>();
                foreach (var type in eventHandlerTypes)
                {
                    var implementation = IoC.Container.GetInstance(type);
                    if (implementation != null) implementations.Add(implementation);
                }
                if (implementations.Any()) handlers[@event] = implementations.ToArray();
            }

            using (var persistenceEngine = new SqlPersistenceEngine(new ConfigurationConnectionFactory("CqrsSampleEventStore"), new MsSqlDialect(),
                new JsonSerializer()))
            {
                var commits = persistenceEngine.GetFrom(new DateTime(2000, 1, 1));
                foreach (var message in commits.SelectMany(x => x.Events))
                {
                    if (!(message.Body is IEvent)) continue;

                    var @event = message.Body;
                    var type = @event.GetType();
                    if (!handlers.ContainsKey(type)) continue;

                    foreach (var eventHandler in handlers[type])
                    {
                        eventHandler.AsDynamic().Handle(@event);
                    }
                }
            }
        }
    }
}