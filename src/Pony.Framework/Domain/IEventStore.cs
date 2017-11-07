using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public interface IEventStore
    {
        void SaveEvent<TAggregate>(IDomainEvent @event) where TAggregate : IAggregateRoot;
        Task SaveEventAsync<TAggregate>(IDomainEvent @event) where TAggregate : IAggregateRoot;
    }
}
