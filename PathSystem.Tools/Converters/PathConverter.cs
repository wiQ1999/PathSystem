using PathSystem.Models;
using PathSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathSystem.Tools.Converters
{
    public class PathConverter
    {
        public Path ConvertToPathModel(List<PathNode> pathNodes, Entity entity)
        {
            List<PathPosition> list = new(pathNodes.Count);

            PathNode tempNode = pathNodes[0];

            foreach (var pathNode in pathNodes)
            {
                float multiplier = 1.0f;

                if (pathNode.X != tempNode.X && pathNode.Y != tempNode.Y)
                    multiplier = 1.4f;

                list.Add(new PathPosition() { PositionX = pathNode.X, PositionY = pathNode.Y, Milliseconds = (int)Math.Ceiling(entity.Speed * multiplier) });

                tempNode = pathNode;
            }

            return new Path() { Entity = entity, PathPositions = list };
        }

        public List<ActivePathPoint> ConvertToActivePathPoints(ICollection<PathPosition> pathPositions)
        {
            List<ActivePathPoint> result = new(pathPositions.Count);

            foreach (var position in pathPositions)
                result.Add(new ActivePathPoint() { PositionX = position.PositionX, PositionY = position.PositionY, Milliseconds = position.Milliseconds });

            return result;
        }

        public PathNode[,] ConvertToPathNodes(IEnumerable<MapPosition> mapModel)
        {
            int maxX = mapModel.Max(m => m.PositionX) + 1;
            int maxY = mapModel.Max(m => m.PositionY) + 1;

            PathNode[,] mapArray = new PathNode[maxX, maxY];

            foreach (MapPosition position in mapModel)
                mapArray[position.PositionX, position.PositionY] = new PathNode(position.PositionX, position.PositionY);

            return mapArray;
        }
    }
}
