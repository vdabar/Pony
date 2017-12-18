using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Mazes.Commands
{
    public class MoveMazeObjects : BaseDomain
    {
        [JsonIgnore]
        public Guid MazeId { get; set; }

        public string Direction { get; set; }
    }
}