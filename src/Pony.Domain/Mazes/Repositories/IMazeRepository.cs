using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pony.Domain.Mazes;
using Pony.Framework.Domain;

namespace Pony.Domain.Repositories
{
    public interface IMazeRepository : IRepository<Maze>
    {
        Task<Maze> GetByIdAsync(Guid Id);

        Task AddAsync(Maze maze);

        Task UpdateAsync(Maze maze);

        Task<IEnumerable<Maze>> BrowseAsnc();
    }
}