using Pony.Domain.Maze;
using Pony.Domain.Services;
using Pony.Services.Services.Maze;
using MazeModel = Pony.Domain.Maze.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Domain.Maze.Commands;

namespace Pony.Services.Services
{
    public class MazeService : IMazeService
    {
        public Cell[] maze;
        public Cell current;
        public Cell next;
        public int height;
        public int width;
        public int difficulty;
        public int maxDifficulty;
        public int ponyIndex = -1;
        public int domokunIndex = -1;
        public int endIndex = -1;
        public Stack<Cell> visitedStack = new Stack<Cell>();
        public List<Stack<Cell>> Paths = new List<Stack<Cell>>();

        public MazeModel CreateMaze(CreateMaze command)
        {
            maxDifficulty = 10;
            width = command.MazeWidth;
            height = command.MazeHeight;
            difficulty = command.Difficulty;
            InitializeMaze();
            Random rnd = new Random();
            ponyIndex = rnd.Next(0, height * width);
            current = maze[ponyIndex];
            visitedStack.Push(current);
            GenerateMaze();
            PlaceObjects();
            PrintMazeToConsole(MazeToString(), height, width);
            return new MazeModel
            {
                data = MazeToString(),
                PonyPosition = ponyIndex,
                DomkunPostion = domokunIndex,
                EndpointPosition = endIndex,
            };
        }

        private void PlaceObjects()
        {
            var maxPath = new Stack<Cell>();
            var maxCount = 0;
            foreach (var path in Paths)
            {
                if (path.Count > maxCount)
                {
                    maxPath = path;
                    maxCount = maxPath.Count;
                }
            }
            var difficultyStep = maxCount / (maxDifficulty + 1);
            var takeout = difficultyStep * (maxDifficulty - difficulty);

            for (int i = 0; i < takeout; i++)
            {
                endIndex = (maxPath.Pop()).Position;
            }
            var pathList = maxPath.ToList();
            Cell enemyCell = null;
            while (enemyCell == null)
            {
                Random rnd = new Random();
                domokunIndex = rnd.Next(0, height * width);
                if (pathList.FirstOrDefault(x => x.Position == domokunIndex) == null)
                {
                    enemyCell = maze[domokunIndex];
                }
            }
            return;
        }

        public void GenerateMaze()
        {
            current.Visited = true;
            next = CheckNeighbours();
            if (next == null)
            {
                Paths.Add(new Stack<Cell>(visitedStack));
                if (visitedStack.Count == 1) return;
                current = visitedStack.Pop();
            }
            else
            {
                RemoveWall();
                current = next;
                visitedStack.Push(current);
            }

            GenerateMaze();
            return;
        }

        private List<string>[] MazeToString()
        {
            var stringMaze = new List<string>[width * height];
            for (int i = 0; i < height * width; i++)
            {
                var cell = maze[i];
                var walls = new List<string>();
                if (cell.NorthWall)
                {
                    walls.Add("north");
                }
                if (cell.WestWall)
                {
                    walls.Add("west");
                }
                stringMaze[i] = walls;
            }
            return stringMaze;
        }

        private void RemoveWall()
        {
            if ((next.Position - current.Position) == 1)//east
            {
                next.WestWall = false;
            }
            if ((next.Position - current.Position) == -1)//west
            {
                current.WestWall = false;
            }
            if ((next.Position - current.Position) > 1)//south
            {
                next.NorthWall = false;
            }
            if ((next.Position - current.Position) < -1)//north
            {
                current.NorthWall = false;
            }
        }

        private Cell CheckNeighbours()
        {
            List<Cell> neighbours = new List<Cell>();
            var x = current.Position % width;
            var y = current.Position / width;

            var westNeighbourIndex = width * y + x - 1;
            var northNeigbourIndex = width * (y - 1) + x;
            var southNeighbourIndex = width * (y + 1) + x;
            var eastNeighbourIndex = width * y + x + 1;

            if (x != 0 && !maze[westNeighbourIndex].Visited)
            {
                neighbours.Add(maze[westNeighbourIndex]);
            }

            if (y != 0 && !maze[northNeigbourIndex].Visited)
            {
                neighbours.Add(maze[northNeigbourIndex]);
            }

            if (y != height - 1 && !maze[southNeighbourIndex].Visited)
            {
                neighbours.Add(maze[southNeighbourIndex]);
            }

            if (x != width - 1 && !maze[eastNeighbourIndex].Visited)
            {
                neighbours.Add(maze[eastNeighbourIndex]);
            }
            if (neighbours.Count > 0)
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(0, neighbours.Count);
                return neighbours[randomIndex];
            }
            return null;
        }

        private void InitializeMaze()
        {
            var size = width * height;
            maze = new Cell[size];
            for (int i = 0; i < size; i++)
            {
                maze[i] = new Cell { Position = i };
            }
        }

        public void PrintMazeToConsole(List<string>[] maze, int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var cell = maze[i * width + j];
                    if (cell.Contains("north"))
                    {
                        Console.Write(@"+---");
                    }
                    else
                    {
                        Console.Write(@"+   ");
                    };
                    if (j == width - 1)
                    {
                        Console.WriteLine(@"+");
                        for (int z = 0; z < width; z++)
                        {
                            var index = i * width + z;
                            cell = maze[index];
                            if (cell.Contains("west"))
                            {
                                if (index == ponyIndex)
                                {
                                    Console.Write(@"| P ");
                                }
                                else if (index == domokunIndex)
                                {
                                    Console.Write(@"| D ");
                                }
                                else if (index == endIndex)
                                {
                                    Console.Write(@"| E ");
                                }
                                else
                                {
                                    Console.Write(@"|   ");
                                }
                            }
                            else
                            {
                                if (index == ponyIndex)
                                {
                                    Console.Write(@"  P ");
                                }
                                else if (index == domokunIndex)
                                {
                                    Console.Write(@"  D ");
                                }
                                else if (index == endIndex)
                                {
                                    Console.Write(@"  E ");
                                }
                                else
                                {
                                    Console.Write(@"    ");
                                }
                            };
                            if (z == width - 1)
                            {
                                Console.WriteLine(@"|");
                            }
                        }
                    }
                }
                if (i == height - 1)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write("+---");
                    }
                    Console.WriteLine("+");
                }
            }
        }
    }
}