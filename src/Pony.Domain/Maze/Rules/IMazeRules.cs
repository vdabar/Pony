using Pony.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze.Rules
{
    public interface IMazeRules:IRules<Maze>
    {
        bool IsPonyNameValidAsync(string name);
    }
}
