using Pony.Domain.Maze;
using Pony.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Data.Repositories
{
    public class MazeRepository : IMazeRepository
    {
        public async Task CreateAsync(Maze maze)
        {
            throw new NotImplementedException();
        }

        public async Task<Maze> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}