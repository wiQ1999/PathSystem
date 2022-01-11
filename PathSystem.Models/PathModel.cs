using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PathSystem.Models
{
    public class PathModel
    {
        public int Id { get; set; }

        public EntityModel Entity { get; set; }

        public List<PathPositionModel> Points { get; set; }

        public bool IsFinished { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
