using System.Text.Json.Serialization;

namespace PathSystem.Models
{
    public class PathPosition
    {
        public int Id { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int Milliseconds { get; set; }

        [JsonIgnore]
        public Path Path { get; set; }
    }
}
