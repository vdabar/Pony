using Pony.Domain.Repositories;
using Pony.Framework.Queries;
using Pony.Reporting.Mazes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Reporting.Mazes.Handlers
{
    public class GetMazeVisualisationHandler : IQueryHandlerAsync<GetMazeVisualisation, MazeVisualisationModel>
    {
        private readonly IMazeRepository _mazeRepository;

        public GetMazeVisualisationHandler(IMazeRepository mazeRepository)
        {
            _mazeRepository = mazeRepository;
        }

        public async Task<MazeVisualisationModel> RetrieveAsync(GetMazeVisualisation query)
        {
            var maze = await _mazeRepository.GetByIdAsync(query.Id);
            return new MazeVisualisationModel
            {
                Maze = maze.ToString()
            };
        }
    }
}