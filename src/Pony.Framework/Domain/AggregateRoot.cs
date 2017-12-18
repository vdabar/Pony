﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Pony.Framework.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public ICollection<IDomainEvent> Events { get; protected set; } = new List<IDomainEvent>();

        protected AggregateRoot()
        {
            Id = Guid.Empty;
        }

        protected AggregateRoot(Guid id)
        {
            if (id == Guid.Empty)
                id = Guid.NewGuid();

            Id = id;
        }

        protected void AddEvent(IDomainEvent @event)
        {
            Events.Add(@event);
        }
    }
}