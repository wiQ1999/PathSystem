using System;

namespace PathSystem.Models
{
    public class ActiveEntityModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Ilość milisekund na pokonanie odległości jednego bloku mapy
        /// </summary>
        public int Speed { get; set; }
    }
}
