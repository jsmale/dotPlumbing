using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Sample.Events;
using EventStore;

namespace Cqrs.Sample.CommandService
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events);
        List<IEvent> GetEventsForAggregate(Guid aggregateId);
    }

    public class EventStore : IEventStore
    {
        readonly IStoreEvents store;

        public EventStore(IStoreEvents store)
        {
            this.store = store;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events)
        {
            using (var stream = store.OpenStream(aggregateId, 0, int.MaxValue))
            {
                foreach (var @event in events)
                {
                    stream.Add(new EventMessage { Body = @event });
                }
                stream.CommitChanges(Guid.NewGuid());
            }
        }

        public List<IEvent> GetEventsForAggregate(Guid aggregateId)
        {
            using (var stream = store.OpenStream(aggregateId, 0, int.MaxValue))
            {
                return stream.CommittedEvents.Select(x => (IEvent)x.Body).ToList();
            }
        }
    }
}