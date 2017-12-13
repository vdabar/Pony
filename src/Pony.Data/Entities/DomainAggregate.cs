using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Data.Entities
{
    public class DomainAggregate
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}