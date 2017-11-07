using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Data.Entities
{
    public class Maze
    {
        public Guid Id { get; set; }
        public int MazeWidth { get; set; }
        public string MazePlayerName { get; set; }
        public int Difficulty { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
