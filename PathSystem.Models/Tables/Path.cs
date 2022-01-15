using PathSystem.Models.Tables;
using System;
using System.Collections.Generic;

namespace PathSystem.Models
{
    public class Path
    {
        public int Id { get; set; }

        public Entity Entity { get; set; }

        public bool IsFinished { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public long PathfindingMillisecond { get; set; }

        public ICollection<PathPosition> PathPositions { get; set; }
    }
}
