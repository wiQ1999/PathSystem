using PathSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PathSystem.Tools
{
    public static class MapHelper
    {
        public static IEnumerable<MapPositionModel> ConvertToMapModel(bool[,] mapArray)
        {
            int destinationX = mapArray.GetLength(0);
            int destinationY = mapArray.GetLength(1);

            List<MapPositionModel> mapModel = new(destinationX * destinationY);

            for (int y = 0; y < destinationY; y++)
            {
                for (int x = 0; x < destinationX; x++)
                {
                    mapModel.Add(new MapPositionModel() { PositionX = x, PositionY = y, Value = mapArray[x, y] });
                }
            }

            return mapModel;
        }

        public static bool[,] ConvertToMapArray(IEnumerable<MapPositionModel> mapModel)
        {
            int maxX = mapModel.Max(m => m.PositionX) + 1;
            int maxY = mapModel.Max(m => m.PositionY) + 1;

            bool[,] mapArray = new bool[maxX, maxY];

            foreach (MapPositionModel position in mapModel)
                mapArray[position.PositionX, position.PositionY] = position.Value;

            return mapArray;
        }
    }
}
