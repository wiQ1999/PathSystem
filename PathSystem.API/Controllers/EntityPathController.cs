using Microsoft.AspNetCore.Mvc;
using PathSystem.API.MapService;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace PathSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityPathController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IEntityPositionRepository _entityPositionRepository;
        private readonly IPathRepository _pathRepository;

        public EntityPathController(IEntityRepository entityRepository, IEntityPositionRepository entityPositionRepository, IPathRepository pathRepository)
        {
            _entityRepository = entityRepository;
            _entityPositionRepository = entityPositionRepository;
            _pathRepository = pathRepository;
        }

        [HttpPost("initialize")]
        public async Task<ActionResult<ActiveEntityPositionModel>> InitializeEntity(ActiveEntityModel activeEntityModel)
        {
            if (activeEntityModel == null || activeEntityModel.Guid == Guid.Empty || activeEntityModel.Speed <= 0)
                return BadRequest();

            var currentEntity = await _entityRepository.GetEntity(activeEntityModel.Guid);

            if (currentEntity == null || currentEntity == default(EntityModel))
                await _entityRepository.AddEntity(new EntityModel()
                {
                    Guid = activeEntityModel.Guid,
                    Name = activeEntityModel.Name,
                    Speed = activeEntityModel.Speed,
                    IsActive = true
                });
            else if (!currentEntity.IsActive)
                return Unauthorized();

            var mapInstance = await MapInstance.GetInstance();
            var mapService = new MapService.MapService(mapInstance.MapArray);
            var randomPosition = mapService.GetRandomStartPosition();

            return Ok(new ActiveEntityPositionModel()
            {
                Entity = activeEntityModel,
                PositionX = randomPosition.X,
                PositionY = randomPosition.Y,
                CreatedDateTime = DateTime.Today
            });
        }

        [HttpGet("map")]
        public async Task<ActionResult<IEnumerable<MapPositionModel>>> GetMap()
        {
            var result = await MapInstance.GetInstance();

            return Ok(result.MapModel);
        }

        [HttpPost("path")]
        public async Task<ActionResult<IEnumerable<PathPositionModel>>> GetPath(ActiveEntityPositionModel activeEntityPositionModel) // TODO
        {
            if (activeEntityPositionModel == null || activeEntityPositionModel.Entity == null || activeEntityPositionModel.Entity.Guid == Guid.Empty || activeEntityPositionModel.Entity.Speed <= 0)
                return BadRequest();

            var currentEntity = await _entityRepository.GetEntity(activeEntityPositionModel.Entity.Guid);

            if (currentEntity == null || !currentEntity.IsActive)
                return Unauthorized();

            var mapInstance = await MapInstance.GetInstance();
            var mapService = new MapService.MapService(mapInstance.MapArray);

            if (!mapService.IsPointClear(new Point(activeEntityPositionModel.PositionX, activeEntityPositionModel.PositionY)))
                return StatusCode(406, $"Position ({activeEntityPositionModel.PositionX}, {activeEntityPositionModel.PositionY}) is loced");

            // obliczenie algorytmu trasy

            // dodanie trasy do bazy

            PathPositionModel[] result = new[] // testowe dane
            {
                new PathPositionModel() { Id = 12, PositionX = 15, PositionY = 71, Milliseconds = 1750 },
                new PathPositionModel() { Id = 13, PositionX = 16, PositionY = 70, Milliseconds = 1750 },
                new PathPositionModel() { Id = 14, PositionX = 17, PositionY = 69, Milliseconds = 1750 },
                new PathPositionModel() { Id = 15, PositionX = 18, PositionY = 68, Milliseconds = 1750 },
                new PathPositionModel() { Id = 16, PositionX = 19, PositionY = 67, Milliseconds = 1750 },
                new PathPositionModel() { Id = 17, PositionX = 20, PositionY = 66, Milliseconds = 1750 },
                new PathPositionModel() { Id = 18, PositionX = 21, PositionY = 65, Milliseconds = 1750 },
                new PathPositionModel() { Id = 19, PositionX = 21, PositionY = 64, Milliseconds = 1600 },
                new PathPositionModel() { Id = 20, PositionX = 21, PositionY = 63, Milliseconds = 1600 },
                new PathPositionModel() { Id = 21, PositionX = 21, PositionY = 62, Milliseconds = 1600 },
                new PathPositionModel() { Id = 22, PositionX = 21, PositionY = 61, Milliseconds = 1600 },
                new PathPositionModel() { Id = 23, PositionX = 21, PositionY = 60, Milliseconds = 1600 },
                new PathPositionModel() { Id = 24, PositionX = 21, PositionY = 59, Milliseconds = 1600 },
                new PathPositionModel() { Id = 25, PositionX = 21, PositionY = 58, Milliseconds = 1600 },
                new PathPositionModel() { Id = 26, PositionX = 21, PositionY = 57, Milliseconds = 1600 },
                new PathPositionModel() { Id = 27, PositionX = 21, PositionY = 56, Milliseconds = 1600 },
                new PathPositionModel() { Id = 28, PositionX = 21, PositionY = 55, Milliseconds = 1600 },
                new PathPositionModel() { Id = 29, PositionX = 21, PositionY = 54, Milliseconds = 1600 },
                new PathPositionModel() { Id = 30, PositionX = 21, PositionY = 53, Milliseconds = 1600 },
                new PathPositionModel() { Id = 31, PositionX = 21, PositionY = 52, Milliseconds = 1600 },
                new PathPositionModel() { Id = 32, PositionX = 21, PositionY = 51, Milliseconds = 1600 },
                new PathPositionModel() { Id = 33, PositionX = 20, PositionY = 50, Milliseconds = 1750 },
                new PathPositionModel() { Id = 34, PositionX = 19, PositionY = 49, Milliseconds = 1750 },
            };

            return Ok(result);
        }


        [HttpPut("position")]
        public async Task<ActionResult<ActiveEntityPositionModel>> AddPosition(ActiveEntityPositionModel activeEntityPositionModel)
        {
            if (activeEntityPositionModel == null || activeEntityPositionModel.Entity == null || activeEntityPositionModel.Entity.Guid == Guid.Empty || activeEntityPositionModel.Entity.Speed <= 0)
                return BadRequest();

            var currentEntity = await _entityRepository.GetEntity(activeEntityPositionModel.Entity.Guid);

            if (currentEntity == null || !currentEntity.IsActive)
                return Unauthorized();

            EntityPositionModel entityPositionModel = new()
            {
                Entity = currentEntity,
                PositionX = activeEntityPositionModel.PositionX,
                PositionY = activeEntityPositionModel.PositionY
            };

            await _entityPositionRepository.AddEntityPosition(entityPositionModel);

            return Ok(activeEntityPositionModel);
        }
    }
}
