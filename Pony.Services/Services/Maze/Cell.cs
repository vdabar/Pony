using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Services.Services.Maze
{
    public class Cell
    {
        public bool NorthWall { get; set; } = true;
        public bool WestWall { get; set; } = true;

        public bool Visited { get; set; } = false;
        public int Position { get; set; }
    }
}