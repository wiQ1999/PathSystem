using Microsoft.AspNetCore.Mvc;
using PathSystem.API.MapService;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using PathSystem.Models.Tables;
using PathSystem.PathFinder;
using PathSystem.Tools.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.API.Controllers
{
    [ApiController]
    [Route("api/entitypath")]
    public class EntityPathController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IEntityPositionRepository _entityPositionRepository;
        private readonly IPathRepository _pathRepository;

        private AStarAlgorithm _pathFinder;

        public EntityPathController(IEntityRepository entityRepository, IEntityPositionRepository entityPositionRepository, IPathRepository pathRepository)
        {
            _entityRepository = entityRepository;
            _entityPositionRepository = entityPositionRepository;
            _pathRepository = pathRepository;
            _pathFinder = new(MapInstance.GetInstance().Result.MapModel);
        }

        [HttpPost("initialize")]
        public async Task<ActionResult<ActiveEntityPositionModel>> InitializeEntity(ActiveEntityModel entity)
        {
            if (CheckActiveEntityModel(entity))
                return BadRequest();

            var entityDB = await _entityRepository.GetEntity(entity.Guid);

            if (entityDB == null || entityDB == default(Entity))
                await _entityRepository.AddEntity(new Entity()
                {
                    Guid = entity.Guid,
                    Name = entity.Name,
                    Speed = entity.Speed,
                    IsActive = true
                });
            else if (!entityDB.IsActive)
                return Unauthorized();

            var startPosition = new MapCalculator((await MapInstance.GetInstance()).MapModel.ToArray()).FindRandomStartPosition();

            if (startPosition == null)
                NotFound("Start position not found after 10 tries");

            return Ok(new ActiveEntityPositionModel()
            {
                Entity = entity,
                PositionX = ((Point)startPosition).X,
                PositionY = ((Point)startPosition).Y,
                CreatedDateTime = DateTime.Today
            });
        }

        [HttpGet("map")]
        public async Task<ActionResult<IEnumerable<MapPosition>>> GetMap()
        {
            return Ok((await MapInstance.GetInstance()).MapModel);
        }

        [HttpPost("path")]
        public async Task<ActionResult<IEnumerable<PathPosition>>> GetPath(ActiveEntityPositionPathModel entityPositionPath)
        {
            if (entityPositionPath == null || entityPositionPath.EntityPosition == null || CheckActiveEntityModel(entityPositionPath.EntityPosition.Entity))
                return BadRequest();

            var entityDB = await _entityRepository.GetEntity(entityPositionPath.EntityPosition.Entity.Guid);
            if (entityDB == null || !entityDB.IsActive)
                return Unauthorized();

            await AddPosition(entityPositionPath.EntityPosition); // dodanie aktualnej lokalziacji jendostki

            foreach (var curentPath in await _pathRepository.GetPaths(entityDB, false)) // zamknięcie otwartych tras jednoski
                await _pathRepository.UpdatePathFinished(curentPath, true);

            Point endPoint = new(entityPositionPath.DestinationX, entityPositionPath.DestinationY);
            if (!new MapCalculator((await MapInstance.GetInstance()).MapModel.ToArray()).IsPointClear(endPoint)) // sprawdzenie dostępności punktu docelowego
                return StatusCode(406, $"Position ({endPoint.X}, {endPoint.Y}) is loced");

            // algorytm wyszukiwania trasy
            var path = _pathFinder.FindPath(entityPositionPath.EntityPosition.PositionX, entityPositionPath.EntityPosition.PositionY, entityPositionPath.DestinationX, entityPositionPath.DestinationY);

            if (path != null && path.Any())
            {
                var pathConverter = new PathConverter();

                var pathDBModel = pathConverter.ConvertToPathModel(path, entityDB); // konwersja na model trasy bazodanowej

                await _pathRepository.AddPath(pathDBModel); // dodanie trasy do bazy dancyh

                var pathResult = pathConverter.ConvertToActivePathPoints(pathDBModel.PathPositions); // konwersja na model rezultatu jednostki

                return Ok(pathResult);
            }

            return NotFound("No path was found");
        }


        [HttpPut("position")]
        public async Task<ActionResult<ActiveEntityPositionModel>> AddPosition(ActiveEntityPositionModel entityPosition)
        {
            if (entityPosition == null || CheckActiveEntityModel(entityPosition.Entity))
                return BadRequest();

            var entityDB = await _entityRepository.GetEntity(entityPosition.Entity.Guid);

            if (entityDB == null || !entityDB.IsActive)
                return Unauthorized();

            EntityPosition entityPositionModel = new()
            {
                Entity = entityDB,
                PositionX = entityPosition.PositionX,
                PositionY = entityPosition.PositionY
            };

            await _entityPositionRepository.AddEntityPosition(entityPositionModel);

            return Ok(entityPosition);
        }

        private bool CheckActiveEntityModel(ActiveEntityModel activeEntityModel) => 
            activeEntityModel == null || activeEntityModel.Guid == Guid.Empty || activeEntityModel.Speed <= 0;
    }
}
