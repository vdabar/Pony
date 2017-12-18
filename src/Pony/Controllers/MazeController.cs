using Microsoft.AspNetCore.Mvc;
using Pony.Domain.Mazes;
using Pony.Domain.Mazes.Commands;
using Pony.Framework.Commands;
using Pony.Framework.Queries;
using Pony.Reporting.Mazes;
using Pony.Reporting.Mazes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Controllers
{
    [Route("pony-challange/[controller]")]
    public class MazeController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IQueryDispatcher _queryDispatcher;

        public MazeController(ICommandSender commandSender, IQueryDispatcher queryDispatcher)
        {
            _commandSender = commandSender;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _queryDispatcher.DispatchAsync<GetMaze, MazeDetailsModel>(new GetMaze { Id = id });

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id}/Print")]
        public async Task<IActionResult> GetVisualisation(Guid id)
        {
            var model = await _queryDispatcher.DispatchAsync<GetMazeVisualisation, MazeVisualisationModel>(new GetMazeVisualisation { Id = id });

            if (model == null)
                return NotFound();

            return Ok(model.Maze);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateMaze model)
        {
            var guid = Guid.NewGuid();
            model.Id = guid;
            _commandSender.Send<CreateMaze, Maze>(model);

            return new OkObjectResult(new { MazeId = guid });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Move(Guid id, [FromBody]MoveMazeObjects model)
        {
            model.MazeId = id;
            await _commandSender.SendAsync<MoveMazeObjects, Maze>(model);
            var state = (await _queryDispatcher.DispatchAsync<GetMaze, MazeDetailsModel>(new GetMaze { Id = model.MazeId })).GameState;

            return new OkObjectResult(state);
        }
    }
}