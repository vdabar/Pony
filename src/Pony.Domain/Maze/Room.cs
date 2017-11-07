using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze
{
    public class Room:ValueObject
    {
        public Guid Id { get; set; }
        public int Position { get; set; }
        public Maze Maze { get; set; }
        IEnumerable<RoomElement> RoomElements { get; set; }
    }
}
