using Microsoft.AspNetCore.Mvc;
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

        public StatusPanelController(IMapRepository mapRepository, IEntityRepository entityRepository)
        {
            _mapRepository = mapRepository;
            _entityRepository = entityRepository;
        }

        [HttpGet("map")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> GetMap() // TODO
        {
            return StatusCode(501, "Póki co nie ma implementacji tego badziewia :D, będzie zwracał tablicę obiektów \"MapPositionModel\", które będzie trzeba sobie przekonwertować na tablicę bool[,]");
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
    }
}
