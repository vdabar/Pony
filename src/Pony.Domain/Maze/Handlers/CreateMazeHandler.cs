using Pony.Domain.Maze.Commands;
using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Framework.Events;
using FluentValidation;
using Pony.Domain.Services;

namespace Pony.Domain.Maze.Handlers
{
    public class CreateMazeHandler : ICommandHandler<CreateMaze>
    {
        private readonly IValidator<CreateMaze> _validator;
        private readonly IMazeService _mazeService;

        public CreateMazeHandler(IValidator<CreateMaze> validator, IMazeService mazeService)
        {
            _validator = validator;
            _mazeService = mazeService;
        }

        public IEnumerable<IEvent> Handle(CreateMaze command)
        {
            var maze = Maze.CreateNew(command, _validator);

            maze = _mazeService.CreateMaze(command);
            //_mazeService.PrintMazeToConsole(maze.data, command.MazeHeight, command.MazeHeight);
            throw new Exception();
            return maze.Events;
        }
    }
}