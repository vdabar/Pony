using FluentValidation;
using Pony.Domain.Mazes.Commands;
using Pony.Domain.Mazes.Events;
using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pony.Domain.Mazes
{
    public class Maze : AggregateRoot
    {
        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }
        public int Difficulty { get; set; }
        public string MazePlayerName { get; set; }
        public int PonyPosition { get; set; }
        public int DomokunPostion { get; set; }
        public int EndpointPosition { get; set; }
        public List<string>[] data { get; set; }
        public GameState GameState { get; set; }

        public Maze()
        {
        }

        public Maze(CreateMaze cmd) : base(cmd.Id)
        {
            MazeWidth = cmd.MazeWidth;
            MazeHeight = cmd.MazeHeight;
            Difficulty = cmd.Difficulty;
            MazePlayerName = cmd.MazePlayerName;
            data = InitializeMazeCreation(cmd);
            GameState = new GameState
            {
                State = State.Active,
                StateResult = "Successfully created"
            };
            AddEvent(new MazeCreated
            {
                AggregateRootId = Id,
            });
        }

        public static Maze CreateNew(CreateMaze cmd, IValidator<CreateMaze> validator)
        {
            validator.ValidateCommand(cmd);
            return new Maze(cmd);
        }

        #region objectsMovement

        public void MoveMazeObjects(MoveMazeObjects cmd, IValidator<MoveMazeObjects> validator)
        {
            validator.ValidateCommand(cmd);
            if (cmd.Direction == "stay")
            {
                GameState.StateResult = "You stayed on the same position only monster moved";
                MoveDomokun();
            }
            else
            {
                if (MovePony(cmd.Direction))
                {
                    MoveDomokun();
                }
            }
            UpdateState();
            AddEvent(new MazeObjectsMoved
            {
                AggregateRootId = Id
            });
        }

        private void UpdateState()
        {
            if (EndpointPosition == PonyPosition)
            {
                GameState.State = State.Finished;
                GameState.StateResult = "Congratulations! You have successfully found escape!";
            }
            if (PonyPosition == DomokunPostion)
            {
                GameState.State = State.Inactive;
                GameState.StateResult = "Sorry. You are dead :(";
            }
        }

        private void MoveDomokun()
        {
            var domokunNeighbours = FindAvailableNeighbours(DomokunPostion);
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, domokunNeighbours.Count);
            DomokunPostion = domokunNeighbours.ElementAt(randomIndex).Key;
        }

        private bool MovePony(string direction)
        {
            var ponyNeighbours = FindAvailableNeighbours(PonyPosition);
            if (ponyNeighbours.ContainsValue(direction))
            {
                PonyPosition = ponyNeighbours.FirstOrDefault(x => x.Value == direction).Key;
                GameState.StateResult = "Move accepted";
                return true;
            }
            else
            {
                GameState.StateResult = "Can't walk in there";
                return false;
            }
        }

        private Dictionary<int, string> FindAvailableNeighbours(int position)
        {
            var neighbours = new Dictionary<int, string>();
            var x = position % MazeWidth;
            var y = position / MazeWidth;

            var westNeighbourIndex = MazeWidth * y + x - 1;
            var northNeigbourIndex = MazeWidth * (y - 1) + x;
            var southNeighbourIndex = MazeWidth * (y + 1) + x;
            var eastNeighbourIndex = MazeWidth * y + x + 1;

            if (x != 0 && !data[position].Contains("west"))//move west
            {
                neighbours.Add(westNeighbourIndex, "west");
            }

            if (y != 0 && !data[position].Contains("north"))//move north
            {
                neighbours.Add(northNeigbourIndex, "north");
            }

            if (y != MazeHeight - 1 && !data[southNeighbourIndex].Contains("north"))//move south
            {
                neighbours.Add(southNeighbourIndex, "south");
            }

            if (x != MazeWidth - 1 && !data[eastNeighbourIndex].Contains("west"))//move east
            {
                neighbours.Add(eastNeighbourIndex, "east");
            }
            return neighbours;
        }

        #endregion objectsMovement

        #region toString

        public override String ToString()
        {
            String stringMaze = "";
            for (int i = 0; i < MazeHeight; i++)
            {
                for (int j = 0; j < MazeWidth; j++)
                {
                    var cell = data[i * MazeWidth + j];
                    if (cell.Contains("north"))
                    {
                        stringMaze += (@"+---");
                    }
                    else
                    {
                        stringMaze += (@"+   ");
                    };
                    if (j == MazeWidth - 1)
                    {
                        stringMaze += ("+\n");
                        for (int z = 0; z < MazeWidth; z++)
                        {
                            var index = i * MazeWidth + z;
                            cell = data[index];
                            if (cell.Contains("west"))
                            {
                                stringMaze += (@"| " + CheckIfObject(index) + " ");
                            }
                            else
                            {
                                stringMaze += (@"  " + CheckIfObject(index) + " ");
                            };
                            if (z == MazeWidth - 1)
                            {
                                stringMaze += ("|\n");
                            }
                        }
                    }
                }
                if (i == MazeHeight - 1)
                {
                    for (int j = 0; j < MazeWidth; j++)
                    {
                        stringMaze += ("+---");
                    }
                    stringMaze += ("+\n");
                }
            }
            return stringMaze;
        }

        private string CheckIfObject(int index)
        {
            if (index == PonyPosition)
            {
                return ("P");
            }
            else if (index == DomokunPostion)
            {
                return ("D");
            }
            else if (index == EndpointPosition)
            {
                return ("E");
            }
            else
            {
                return (@" ");
            }
        }

        #endregion toString

        #region mazeCreation

        public List<string>[] InitializeMazeCreation(CreateMaze cmd)
        {
            Cell[] maze;
            Cell current;
            var visitedStack = new Stack<Cell>();
            var paths = new List<Stack<Cell>>();
            maze = InitializeMaze();
            Random rnd = new Random();
            PonyPosition = rnd.Next(0, MazeHeight * MazeWidth);
            current = maze[PonyPosition];
            visitedStack.Push(current);
            PlaceObjects(paths, maze);
            return MazeToString(GenerateMaze(maze, current, null, visitedStack, paths));
        }

        private List<string>[] MazeToString(Cell[] maze)
        {
            var stringMaze = new List<string>[MazeWidth * MazeHeight];
            for (int i = 0; i < MazeHeight * MazeWidth; i++)
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

        private void PlaceObjects(List<Stack<Cell>> paths, Cell[] maze)
        {
            var maxPath = new Stack<Cell>();
            var maxCount = 0;
            foreach (var path in paths)
            {
                if (path.Count > maxCount)
                {
                    maxPath = path;
                    maxCount = maxPath.Count;
                }
            }
            var difficultyStep = maxCount / 11;
            var takeout = difficultyStep * (Difficulty + 1);

            for (int i = 0; i < takeout; i++)
            {
                var a = maxPath.Pop();
                EndpointPosition = a.Position;
            }
            var pathList = maxPath.ToList();
            Cell enemyCell = null;
            while (enemyCell == null)
            {
                Random rnd = new Random();
                DomokunPostion = rnd.Next(0, MazeHeight * MazeWidth);
                if (pathList.FirstOrDefault(x => x.Position == DomokunPostion) == null)
                {
                    enemyCell = maze[DomokunPostion];
                }
            }
        }

        private Cell[] GenerateMaze(Cell[] maze, Cell current, Cell next, Stack<Cell> visitedStack, List<Stack<Cell>> paths)
        {
            current.Visited = true;
            next = CheckNeighbours(maze, current);
            if (next == null)
            {
                paths.Add(new Stack<Cell>(visitedStack));
                if (visitedStack.Count == 1) return maze;
                current = visitedStack.Pop();
            }
            else
            {
                RemoveWall(current, next);
                current = next;
                visitedStack.Push(current);
            }

            GenerateMaze(maze, current, next, visitedStack, paths);
            return maze;
        }

        private void RemoveWall(Cell current, Cell next)
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

        private Cell CheckNeighbours(Cell[] maze, Cell current)
        {
            List<Cell> neighbours = new List<Cell>();
            var x = current.Position % MazeWidth;
            var y = current.Position / MazeWidth;

            var westNeighbourIndex = MazeWidth * y + x - 1;
            var northNeigbourIndex = MazeWidth * (y - 1) + x;
            var southNeighbourIndex = MazeWidth * (y + 1) + x;
            var eastNeighbourIndex = MazeWidth * y + x + 1;

            if (x != 0 && !maze[westNeighbourIndex].Visited)
            {
                neighbours.Add(maze[westNeighbourIndex]);
            }

            if (y != 0 && !maze[northNeigbourIndex].Visited)
            {
                neighbours.Add(maze[northNeigbourIndex]);
            }

            if (y != MazeHeight - 1 && !maze[southNeighbourIndex].Visited)
            {
                neighbours.Add(maze[southNeighbourIndex]);
            }

            if (x != MazeWidth - 1 && !maze[eastNeighbourIndex].Visited)
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

        private Cell[] InitializeMaze()
        {
            var size = MazeWidth * MazeHeight;
            var maze = new Cell[size];
            for (int i = 0; i < size; i++)
            {
                maze[i] = new Cell { Position = i };
            }
            return maze;
        }

        #endregion mazeCreation
    }
}