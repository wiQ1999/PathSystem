using System;
using System.Collections.Generic;

namespace PathSystem.Models
{
    public class PathModel
    {
        public int Id { get; set; }

        public EntityModel Entity { get; set; }

        public List<PathPositionModel> Points { get; set; }

        public bool IsFinished { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
