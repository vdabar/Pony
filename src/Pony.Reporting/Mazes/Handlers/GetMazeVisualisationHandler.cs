using Pony.Domain.Repositories;
using Pony.Framework.Exceptions;
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
            if (maze == null)
            {
                throw new ApiException($"Maze with id: {query.Id} doesn't exists", 404);
            }
            return new MazeVisualisationModel
            {
                Maze = maze.ToString()
            };
        }
    }
}