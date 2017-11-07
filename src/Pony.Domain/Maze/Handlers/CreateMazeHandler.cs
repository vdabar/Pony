using Pony.Domain.Maze.Commands;
using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Framework.Events;
using FluentValidation;

namespace Pony.Domain.Maze.Handlers
{
    public class CreateMazeHandler : ICommandHandlerAsync<CreateMaze>
    {
        private readonly IValidator<CreateMaze> _validator;

        public CreateMazeHandler(IValidator<CreateMaze> validator)
        {
            _validator = validator;
        }
        public async Task<IEnumerable<IEvent>> HandleAsync(CreateMaze command)
        {
            var maze = Maze.CreateNew(command, _validator);

            return maze.Events;
        }
    }
}
