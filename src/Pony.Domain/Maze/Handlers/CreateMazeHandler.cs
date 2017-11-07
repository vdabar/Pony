using Pony.Domain.Maze.Commands;
using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Framework.Events;

namespace Pony.Domain.Maze.Handlers
{
    public class CreateMazeHandler : ICommandHandlerAsync<CreateMaze>
    {
        public CreateMazeHandler()
        {

        }
        public Task<IEnumerable<IEvent>> HandleAsync(CreateMaze command)
        {
            throw new NotImplementedException();
        }
    }
}
