using FluentValidation;
using Pony.Domain.Maze.Commands;
using Pony.Domain.Maze.Events;
using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze
{
    public class Maze : AggregateRoot
    {
        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }
        public int PonyPosition { get; set; }
        public int DomkunPostion { get; set; }
        public int EndpointPosition { get; set; }
        public string MazePlayerName { get; set; }

        public List<string>[] data { get; set; }

        public Maze()
        {
        }

        public Maze(CreateMaze cmd) : base(cmd.Id)
        {
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
    }
}