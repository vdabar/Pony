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
    public class Maze:AggregateRoot
    {
        public Guid Id { get; set; }
        public int MazeWidth { get; set; }
        public int MazeHeight { get; set; }
        public string MazePlayerName { get; set; }
        public int Difficulty { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

        public Maze()
        {

        }

        public Maze(CreateMaze cmd):base(cmd.Id)
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
