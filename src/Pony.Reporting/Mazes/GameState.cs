using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Reporting.Mazes
{
    public enum State
    {
        Active,
        Inactive,
        Finished
    }

    public class GameState
    {
        public State State { get; set; }
        public String StateResult { get; set; }
    }
}