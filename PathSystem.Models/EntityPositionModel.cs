using System.ComponentModel.DataAnnotations;

namespace PathSystem.Models
{
    public class EntityPositionModel
    {
        public int Id { get; set; }

        public EntityModel Entity { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
