using Pony.Framework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Reporting.Mazes.Queries
{
    public class GetMazeVisualisation : IQuery
    {
        public Guid Id { get; set; }
    }
}