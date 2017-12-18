using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Mazes.Events
{
    public class MazeCreated:DomainEvent
    {
        public Guid Id { get; set; }
    }
}
