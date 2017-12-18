using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pony.Domain.Mazes;
using Pony.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeDbEntity = Pony.Data.Entities.Maze;

namespace Pony.Data.Repositories
{
    public class MazeRepository : IMazeRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public MazeRepository(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Maze> GetByIdAsync(Guid Id)
        {
            var dbEntity = await Collection
                 .AsQueryable()
                 .FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<Maze>(dbEntity);
        }

        public async Task AddAsync(Maze maze)
        {
            var dbEntity = _mapper.Map<MazeDbEntity>(maze);
            await Collection.InsertOneAsync(dbEntity);
        }

        public Task<IEnumerable<Maze>> BrowseAsnc()
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Maze>> BrowseAsnc()
        //=> await Collection
        //    .AsQueryable()
        //    .ToListAsync();
        public async Task UpdateAsync(Maze maze)
        {
            var dbEntity = _mapper.Map<MazeDbEntity>(maze);
            await Collection.ReplaceOneAsync(x => x.Id == maze.Id, dbEntity);
        }

        private IMongoCollection<MazeDbEntity> Collection
            => _database.GetCollection<MazeDbEntity>("Mazes");
    }
}