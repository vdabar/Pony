using AutoMapper;
using Pony.Domain.Mazes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeDbEntity = Pony.Data.Entities.Maze;

namespace Pony.Data
{
    public class DomainAutoMapperProfile : Profile
    {
        public DomainAutoMapperProfile()
        {
            CreateMap<Maze, MazeDbEntity>();
            CreateMap<MazeDbEntity, Maze>().ConstructUsing(x => new Maze());
        }
    }
}