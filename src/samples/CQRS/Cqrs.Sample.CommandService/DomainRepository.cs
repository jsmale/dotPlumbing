using System;
using Cqrs.Sample.Domain;

namespace Cqrs.Sample.CommandService
{
    public interface IDomainRepository
    {
        void Save(AggregateRoot aggregate);
        T GetById<T>(Guid id) where T : AggregateRoot, new();
    }

    public class DomainRepository : IDomainRepository
    {
        private readonly IEventStore storage;

        public DomainRepository(IEventStore storage)
        {
            this.storage = storage;
        }

        public void Save(AggregateRoot aggregate)
        {
            storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges());
            aggregate.MarkChangesAsCommitted();
        }

        public T GetById<T>(Guid id) where T : AggregateRoot, new()
        {
            var obj = new T();
            var e = storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}