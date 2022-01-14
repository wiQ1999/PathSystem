using Microsoft.AspNetCore.Mvc;
using PathSystem.API.MapService;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using PathSystem.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.API.Controllers
{
    [ApiController]
    [Route("api/statuspanel")]
    public class StatusPanelController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IPathRepository _pathRepository;

        public StatusPanelController(IEntityRepository entityRepository, IPathRepository pathRepository)
        {
            _entityRepository = entityRepository;
            _pathRepository = pathRepository;
        }

        [HttpGet("map")]
        public async Task<ActionResult<IEnumerable<MapPosition>>> GetMap()
        {
            var result = await MapInstance.GetInstance();

            return Ok(result.MapModel);
        }

        [HttpGet("entities")]
        public async Task<ActionResult<IEnumerable<Entity>>> GetEntities()
        {
            return Ok(await _entityRepository.GetEntities());
        }

        [HttpGet("paths")]
        public async Task<ActionResult<IEnumerable<Path>>> GetPaths()
        {
            return Ok(await _pathRepository.GetPaths(false));
        }
    }
}
