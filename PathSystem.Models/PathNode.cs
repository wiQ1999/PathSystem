namespace PathSystem.Models
{
    public class PathNode
    {
        public PathNode Parent { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool Blocked { get; set; }

        public int GoalConst { get; set; }

        public int HeuristicCost { get; set; }

        public int CostsSum => GoalConst + HeuristicCost;

        public PathNode(int x, int y)
        {
            X = x;
            Y = y;
            GoalConst = int.MaxValue;
        }

        public override string ToString()
        {
            string result = $"({X}, {Y})";

            if (Parent != null)
                result += $" -> {Parent}";

            return result;
        }
    }
}
