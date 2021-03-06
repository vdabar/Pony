﻿using Pony.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public interface IDomainEvent : IEvent
    {
        Guid AggregateRootId { get; set; }
        int Version { get; set; }
    }
}
