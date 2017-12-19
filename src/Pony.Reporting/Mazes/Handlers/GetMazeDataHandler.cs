using AutoMapper;
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
    public class GetMazeDataHandler : IQueryHandlerAsync<GetMaze, MazeDetailsModel>
    {
        private readonly IMazeRepository _mazeRepository;
        private readonly IMapper _mapper;

        public GetMazeDataHandler(IMazeRepository mazeRepository, IMapper mapper)
        {
            _mazeRepository = mazeRepository;
            _mapper = mapper;
        }

        public async Task<MazeDetailsModel> RetrieveAsync(GetMaze query)
        {
            var dbEntity = await _mazeRepository.GetByIdAsync(query.Id);
            if (dbEntity == null)
            {
                throw new ApiException($"Maze with id: {query.Id} doesn't exists", 404);
            }
            return _mapper.Map<MazeDetailsModel>(dbEntity);
        }
    }
}