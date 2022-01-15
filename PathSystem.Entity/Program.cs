using PathSystem.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace PathSystem.Entity
{
    class Program
    {
        static Random RND = new();

        static ConfigService ConfigService = new();

        static APIService APIService = new();

        static ActiveEntityPositionPathModel Entity = new() { EntityPosition = new() { Entity = new() } };

        static MapPosition[] Map = null;

        static int MapWith = 0;

        static int MapHeight = 0;

        static void Main(string[] args)
        {
            bool autoConfig = ConfigService.IsAutoConfig();

            Entity.EntityPosition.Entity =  ConfigService.CreateEntity(autoConfig);

            if (APIService.InitializeEntity(Entity.EntityPosition.Entity).StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Błąd inicjalizacji");
                return;
            }

            Console.WriteLine($"Zainicjowano jednostkę {Entity.EntityPosition.Entity.Name}!");

            var map = APIService.GetMap().ToArray();

            Map = map;
            MapWith = map.Max(x => x.PositionX) + 1;
            MapHeight = map.Max(x => x.PositionY) + 1;

            Console.WriteLine($"Pobrano mapę o wymiarach {MapWith} x {MapHeight}");

            InfiniteLoop();
        }

        static void InfiniteLoop()
        {
            while (true)
            {
                PathPosition[] path = null;
                ActiveEntityPositionPathModel destination = null;

                while (true)
                {
                    destination = CreateDestinationModel();
                    Console.WriteLine($"Wyszukiwanie ścieżki do punktu ({destination.DestinationX}, {destination.DestinationY})");

                    var respose = APIService.GetPath(destination);
                    if (respose.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine("Błąd autoryzacji");
                        return;
                    }
                    else if (respose.StatusCode == System.Net.HttpStatusCode.OK && respose.Data != null)
                    {
                        path = respose.Data.ToArray();
                        break;
                    }
                }

                var pathTimeSum = path.Sum(x => x.Milliseconds);
                Console.WriteLine($"Znaleziono trasę z czasem {pathTimeSum} milisekund do przebycia");
                DateTime timeToWait = DateTime.Now.AddMilliseconds(pathTimeSum);

                while (DateTime.Now < timeToWait)
                {
                    Thread.Sleep(1000);

                    var calculation = (timeToWait - DateTime.Now).TotalMilliseconds;
                    int nodeSum = 0;

                    foreach (var node in path)
                    {
                        nodeSum += node.Milliseconds;

                        if (nodeSum > pathTimeSum - calculation)
                        {
                            Entity.EntityPosition.PositionX = node.PositionX;
                            Entity.EntityPosition.PositionY = node.PositionY;
                            break;
                        }
                    }

                    UpdatePosition();
                }

                Entity.EntityPosition.PositionX = destination.DestinationX;
                Entity.EntityPosition.PositionY = destination.DestinationY;
                UpdatePosition();
            }
        }

        static void UpdatePosition()
        {
            Entity.EntityPosition.CreatedDateTime = DateTime.Now;
            var respone = APIService.PutPosition(Entity.EntityPosition);

            if (respone.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Błąd autoryzacji");
                return;
            }

            var position = respone.Data;
            if (position != null)
                Console.WriteLine($"Zaktualizowano pozycję ({position.PositionX}, {position.PositionY})");
        }

        static ActiveEntityPositionPathModel CreateDestinationModel() 
        {
            Point point = GetRandomDestination();
            Entity.EntityPosition.CreatedDateTime = DateTime.Now;
            return new() { DestinationX = point.X, DestinationY = point.Y, EntityPosition = Entity.EntityPosition };
        }

        static Point GetRandomDestination() =>
             new(RND.Next(0, MapWith + 1), RND.Next(0, MapHeight + 1));
    }
}
