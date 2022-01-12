using System;

namespace PathSystem.Models
{
    public class ActiveEntityPositionModel
    {
        public ActiveEntityModel Entity { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
