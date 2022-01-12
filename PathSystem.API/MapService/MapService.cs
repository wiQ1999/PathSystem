using PathSystem.Database.Repositories;
using System;
using System.Drawing;
using System.Linq;

namespace PathSystem.API.MapService
{
    public class MapService
    {
        private bool[,] _map;

        private EFPathRepository _pathRepository;

        public MapService(bool[,] mapArray)
        {
            _map = mapArray;
            _pathRepository = new EFPathRepository(new Database.EFContext());
        }

        public Point GetRandomStartPosition()
        {
            Random rnd = new();

            while (true)
            {
                int randomX = rnd.Next(0, _map.GetLength(0) + 1);
                int randomY = rnd.Next(0, _map.GetLength(1) + 1);

                Point result = new(randomX, randomY);

                if (IsPointClear(result))
                    return result;
            }
        }

        public bool IsPointClear(Point position)
        {
            if (position.X >= _map.GetLength(0) || position.Y >= _map.GetLength(1))
                return false;

            if (!_map[position.X, position.Y])
                return false;

            var paths = _pathRepository.GetPaths(false).Result;

            if (paths.Any(path => path.Points.Any(point => point.PositionX == position.X && point.PositionY == position.Y)))
                return false;

            return true;
        }
    }
}
