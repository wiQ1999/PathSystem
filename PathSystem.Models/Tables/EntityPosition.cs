using PathSystem.Models.Tables;
using System;

namespace PathSystem.Models
{
    public class EntityPosition
    {
        public int Id { get; set; }

        public Entity Entity { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
