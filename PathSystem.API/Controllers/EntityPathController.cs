using Microsoft.AspNetCore.Mvc;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityPathController : ControllerBase
    {
        private readonly IMapRepository _mapRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IEntityPositionRepository _entityPositionRepository;
        private readonly IPathRepository _pathRepository;

        public EntityPathController(IMapRepository mapRepository, IEntityRepository entityRepository, IEntityPositionRepository entityPositionRepository, IPathRepository pathRepository)
        {
            _mapRepository = mapRepository;
            _entityRepository = entityRepository;
            _entityPositionRepository = entityPositionRepository;
            _pathRepository = pathRepository;
        }

        [HttpGet("Map")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> GetMap() // TODO
        {
            return StatusCode(501, "Będzie zwracał tablicę obiektów \"IEnumerable<MapPositionModel>\", które będzie trzeba sobie przekonwertować na tablicę bool[,]");
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> InitializeEntity(EntityModel entity) // TODO
        {
            if (entity == null)
                return BadRequest();

            if (_entityRepository.GetEntity(entity.Id) == null)
                await _entityRepository.AddEntity(entity);

            return StatusCode(201, "Będzie zwracał tablicę obiektów \"IEnumerable<MapPositionModel>\", które będzie trzeba sobie przekonwertować na tablicę bool[,]");
            //return await GetMap();
        }

        [HttpPost("Path")]
        public async Task<ActionResult<IEnumerable<PathPositionModel>>> GetPath(EntityPositionModel entityPosition) // TODO
        {
            if (entityPosition == null)
                return BadRequest();

            // sprawdzenie na pamie czy punkt nie jest == false

            // obliczenie algorytmem trasy

            return StatusCode(201, "Będzie zwracał tablicę obiektów \"IEnumerable<PathPositionModel>\", które będą odpowiadać liście kolejnych punktów trasy do przebycia");
        }


        [HttpPatch("Position")] // lub HttpPut
        public async Task<ActionResult<IEnumerable<PathPositionModel>>> UpdatePosition(EntityPositionModel entityPosition)
        {
            if (entityPosition == null)
                return BadRequest();

            return Ok(_entityPositionRepository.AddEntityPosition(entityPosition));
        }
    }
}
