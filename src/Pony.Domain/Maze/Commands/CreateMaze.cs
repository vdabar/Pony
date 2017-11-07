using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze.Commands
{
    public class CreateMaze:ICommand
    {
        public Guid Id { get; set; }
        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }
        public string MazePlayerName { get; set; }
        public int Difficulty { get; set; }
    }
}
