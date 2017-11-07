using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze
{
    public class RoomElement:ValueObject
    {
        public Guid Id { get; set; }
        public ElementNames ElementName { get; set; }
        public Room Room { get; set; }
    }
    public enum ElementNames
    {
        RightWall,
        LeftWall,
        NorthWall,
        WestWall,
        Pony,
        Domokun,
        E
    }
}
