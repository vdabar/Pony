using FluentValidation;
using Pony.Domain.Mazes.Commands;
using Pony.Domain.Repositories;
using Pony.Framework.Commands;
using Pony.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Mazes.Handlers
{
    public class MoveMazeObjectsHandler : ICommandHandlerAsync<MoveMazeObjects>
    {
        private readonly IMazeRepository _mazeRepository;
        private readonly IValidator<MoveMazeObjects> _validator;

        public MoveMazeObjectsHandler(IMazeRepository mazeRepository, IValidator<MoveMazeObjects> validator)
        {
            _mazeRepository = mazeRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<IEvent>> HandleAsync(MoveMazeObjects command)
        {
            var maze = await _mazeRepository.GetByIdAsync(command.MazeId);
            maze.MoveMazeObjects(command, _validator);
            await _mazeRepository.UpdateAsync(maze);
            return maze.Events;
        }
    }
}