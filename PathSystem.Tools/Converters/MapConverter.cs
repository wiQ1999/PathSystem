using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathSystem.Tools.Converters
{
    public class MapConverter
    {
        public IEnumerable<MapPosition> ConvertToMapModel(bool[,] mapArray)
        {
            int destinationX = mapArray.GetLength(0);
            int destinationY = mapArray.GetLength(1);

            List<MapPosition> mapModel = new(destinationX * destinationY);

            for (int y = 0; y < destinationY; y++)
            {
                for (int x = 0; x < destinationX; x++)
                {
                    mapModel.Add(new MapPosition() { PositionX = x, PositionY = y, Value = mapArray[x, y] });
                }
            }

            return mapModel;
        }

        public bool[,] ConvertToMapArray(IEnumerable<MapPosition> mapModel)
        {
            int maxX = mapModel.Max(m => m.PositionX) + 1;
            int maxY = mapModel.Max(m => m.PositionY) + 1;

            bool[,] mapArray = new bool[maxX, maxY];

            foreach (MapPosition position in mapModel)
                mapArray[position.PositionX, position.PositionY] = position.Value;

            return mapArray;
        }

        
    }
}
