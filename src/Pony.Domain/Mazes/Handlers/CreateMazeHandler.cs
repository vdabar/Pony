using Pony.Domain.Mazes.Commands;
using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Framework.Events;
using FluentValidation;
using Pony.Domain.Repositories;

namespace Pony.Domain.Mazes.Handlers
{
    public class CreateMazeHandler : ICommandHandler<CreateMaze>
    {
        private readonly IValidator<CreateMaze> _validator;
        private readonly IMazeRepository _mazeRepository;

        public CreateMazeHandler(IValidator<CreateMaze> validator, IMazeRepository mazeRepository)
        {
            _validator = validator;
            _mazeRepository = mazeRepository;
        }

        public IEnumerable<IEvent> Handle(CreateMaze command)
        {
            var maze = Maze.CreateNew(command, _validator);

            Console.WriteLine(maze.ToString());
            _mazeRepository.AddAsync(maze);

            return maze.Events;
        }
    }
}