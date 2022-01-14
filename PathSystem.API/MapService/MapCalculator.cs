using PathSystem.Database.Repositories;
using PathSystem.Models;
using System;
using System.Drawing;
using System.Linq;

namespace PathSystem.API.MapService
{
    public class MapCalculator
    {
        private readonly MapPosition[] _map;
        private readonly int _mapWith;
        private readonly int _mapHeight;
        private PathRepository _pathRepository = new(new Database.EFContext());

        public MapCalculator(MapPosition[] mapArray)
        {
            _map = mapArray;
            _mapWith = _map.Max(m => m.PositionX) + 1;
            _mapHeight = _map.Max(m => m.PositionY) + 1;
        }

        public Point? FindRandomStartPosition()
        {
            Random rnd = new();

            for (int i = 0; i < 10; i++)
            {
                int randomX = rnd.Next(0, _mapWith);
                int randomY = rnd.Next(0, _mapHeight);

                Point result = new(randomX, randomY);

                if (IsPointClear(result))
                    return result;
            }

            return null;
        }

        public bool IsPointClear(Point position)
        {
            if (position.X < 0 || position.X >= _mapWith || position.Y < 0 || position.Y >= _mapHeight)
                return false;

            if (!_map.First(m => m.PositionX == position.X && m.PositionY == position.Y).Value)
                return false;

            var paths = _pathRepository.GetPaths(false).Result;

            if (paths.Any(path => path.PathPositions.Any(point => point.PositionX == position.X && point.PositionY == position.Y)))
                return false;

            return true;
        }
    }
}
