using System;
using System.ComponentModel.DataAnnotations;

namespace PathSystem.Models
{
    public class EntityModel
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public float Speed { get; set; }

        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
