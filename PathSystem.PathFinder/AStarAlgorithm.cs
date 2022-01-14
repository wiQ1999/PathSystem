using PathSystem.Models;
using PathSystem.Tools.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathSystem.PathFinder
{
    public class AStarAlgorithm
    {
        private const int STRAIGHT_COST = 10;
        private const int DIAGONAL_COST = 14;

        private readonly PathNode[,] _map;
        private List<PathNode> _openNodes;
        private List<PathNode> _closedNodes;

        public AStarAlgorithm(IEnumerable<MapPosition> map)
        {
            _map = new PathConverter().ConvertToPathNodes(map);
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode endNode = _map[endX, endY];
            PathNode startNode = _map[startX, startY];
            startNode.GoalConst = 0;
            startNode.HeuristicCost = CalculateHeuristicDistance(startNode, endNode);

            _openNodes = new List<PathNode>() { startNode };
            _closedNodes = new List<PathNode>();

            while (_openNodes.Any())
            {
                PathNode currentNode = GetLowestCostsSum(_openNodes);

                if (currentNode.X == endX && currentNode.Y == endY)
                    return BuildFinishPathNodes(endNode);

                _openNodes.Remove(currentNode);
                _closedNodes.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode))
                {
                    if (_closedNodes.Contains(neighbourNode)) 
                        continue;

                    int tempGoalCost = currentNode.GoalConst + CalculateHeuristicDistance(currentNode, neighbourNode);

                    if (tempGoalCost < neighbourNode.GoalConst)
                    {
                        neighbourNode.Parent = currentNode;
                        neighbourNode.GoalConst = tempGoalCost;
                        neighbourNode.HeuristicCost = CalculateHeuristicDistance(neighbourNode, endNode);

                        if (!_openNodes.Contains(neighbourNode))
                            _openNodes.Add(neighbourNode);
                    }
                }
            }

            return null;
        }

        private int CalculateHeuristicDistance(PathNode startNode, PathNode endNode)
        {
            int xDistance = Math.Abs(startNode.X - endNode.X);
            int yDistance = Math.Abs(startNode.Y - endNode.Y);
            int remaining = Math.Abs(xDistance - yDistance);

            return DIAGONAL_COST * Math.Min(xDistance, yDistance) + STRAIGHT_COST * remaining;
        }

        private PathNode GetLowestCostsSum(List<PathNode> pathNodes) => pathNodes.OrderBy(pn => pn.CostsSum).FirstOrDefault();

        private List<PathNode> GetNeighbours(PathNode currentNode)
        {
            List<PathNode> result = new List<PathNode>();

            if (currentNode.X - 1 >= 0) // lewa
            {
                result.Add(_map[currentNode.X - 1, currentNode.Y]);

                if (currentNode.Y - 1 >= 0) // lewa dół
                    result.Add(_map[currentNode.X - 1, currentNode.Y - 1]);

                if (currentNode.Y + 1 < _map.GetLength(1)) // lewa góra
                    result.Add(_map[currentNode.X - 1, currentNode.Y + 1]);
            }

            if (currentNode.X + 1 < _map.GetLength(0)) // prawa
            {
                result.Add(_map[currentNode.X + 1, currentNode.Y]);

                if (currentNode.Y - 1 >= 0) // prawa dół
                    result.Add(_map[currentNode.X + 1, currentNode.Y - 1]);

                if (currentNode.Y + 1 < _map.GetLength(1)) // prawa góra
                    result.Add(_map[currentNode.X + 1, currentNode.Y + 1]);
            }

            if (currentNode.Y - 1 >= 0) // dół
                result.Add(_map[currentNode.X, currentNode.Y - 1]);

            if (currentNode.Y + 1 < _map.GetLength(1)) // góra
                result.Add(_map[currentNode.X, currentNode.Y + 1]);

            return result;
        }

        private List<PathNode> BuildFinishPathNodes(PathNode endNode)
        {
            List<PathNode> result = new();

            PathNode currentNode = endNode;
            
            while (currentNode.Parent != null)
            {
                result.Add(currentNode.Parent);
                currentNode = currentNode.Parent;
            }

            result.Reverse();

            return result;
        }
    }
}
