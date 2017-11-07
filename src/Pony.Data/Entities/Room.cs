using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Data.Entities
{
    public class Room:IEntity
    {
        public Guid Id { get; set; }
        public int Position { get; set; }
        public Maze Maze { get; set; }
        IEnumerable<RoomElement> RoomElements { get; set; }
}
}
