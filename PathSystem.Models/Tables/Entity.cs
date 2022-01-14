using System;
using System.ComponentModel.DataAnnotations;

namespace PathSystem.Models.Tables
{
    public class Entity
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Ilość milisekund na pokonanie odległości jednego bloku mapy
        /// </summary>
        public int Speed { get; set; }

        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
