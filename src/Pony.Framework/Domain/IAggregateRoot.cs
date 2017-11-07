using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        ICollection<IDomainEvent> Events { get; }
    }
}
