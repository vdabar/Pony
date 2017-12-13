using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Maze
{
    public enum State
    {
        Active,
        Inactive
    }

    public class GameState
    {
        public State State { get; set; }
        public String StateResult { get; set; }
    }
}