using System;
using System.Collections.Generic;
using Cqrs.Sample.Common.Infrastructure;
using Cqrs.Sample.Events;
using Plumbing.Utility;

namespace Cqrs.Sample.Domain
{
    public abstract class AggregateRoot
    {        
        private readonly List<IEvent> changes = new List<IEvent>();
        public Guid Id { get; protected set; }

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            return changes;
        }

        public void MarkChangesAsCommitted()
        {
            changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(IEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) changes.Add(@event);
        }

        protected static Guid GuidComb()
        {
            return GuidCombGenerator.GenerateComb();
        }
    }
}