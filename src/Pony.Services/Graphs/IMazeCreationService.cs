using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Services.Graphs
{
    public interface IMazeCreationService
    {
         IList<IList<String>> GetMazeData(int width, int height);
    }
}
