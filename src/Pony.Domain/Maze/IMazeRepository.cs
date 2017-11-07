using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze
{
    public interface IMazeRepository
    {
        Maze GetById(Guid Id);

        void Create(Maze maze);
    }
}
