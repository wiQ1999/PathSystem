using Microsoft.AspNetCore.Mvc;
using PathSystem.API.MapService;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusPanelController : ControllerBase
    {
        private readonly IMapRepository _mapRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IPathRepository _pathRepository;

        public StatusPanelController(IMapRepository mapRepository, IEntityRepository entityRepository, IPathRepository pathRepository)
        {
            _mapRepository = mapRepository;
            _entityRepository = entityRepository;
            _pathRepository = pathRepository;
        }

        [HttpGet("map")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> GetMap()
        {
            var result = await MapInstance.GetInstance();

            return Ok(result.MapModel);
        }

        [HttpPost("map")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> AddMap(IEnumerable<MapPositionModel> mapModels) // TODO
        {
            if (mapModels == null)
                return BadRequest();

            await _mapRepository.AddMap(mapModels);

            // usunięcie wszystkich tras lub wszystkich obiektów z mapy

            return await GetMap();
        }

        [HttpGet("entities")]
        public async Task<ActionResult<IEnumerable<EntityModel>>> GetEntities()
        {
            return Ok(await _entityRepository.GetEntities());
        }

        [HttpGet("entities/{id:int}")]
        public async Task<ActionResult<EntityModel>> GetEntity(int id)
        {
            return Ok(await _entityRepository.GetEntity(id));
        }

        [HttpPatch("entitie")]
        public async Task<ActionResult<EntityModel>> UpdateEntityActivity(EntityModel entityModel)
        {
            if (entityModel == null)
                return BadRequest();

            return await _entityRepository.UpdateEntityActivity(entityModel);
        }

        [HttpGet("paths")]
        public async Task<ActionResult<IEnumerable<PathModel>>> GetPaths()
        {
            return Ok(await _pathRepository.GetPaths(false));
        }        
    }
}
