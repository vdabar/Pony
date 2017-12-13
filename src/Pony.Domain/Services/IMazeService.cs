using Pony.Domain.Maze;
using MazeModel = Pony.Domain.Maze.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Domain.Maze.Commands;

namespace Pony.Domain.Services
{
    public interface IMazeService
    {
        MazeModel CreateMaze(CreateMaze command);

        void PrintMazeToConsole(List<string>[] maze, int height, int width);
    }
}