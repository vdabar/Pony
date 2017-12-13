using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Domain.Maze;

namespace Pony.Domain.Repositories
{
    public interface IMazeRepository
    {
        Task<Maze.Maze> GetByIdAsync(Guid Id);

        Task CreateAsync(Maze.Maze maze);
    }
}