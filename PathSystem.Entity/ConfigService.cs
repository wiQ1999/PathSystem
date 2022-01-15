using PathSystem.Models;
using System;
using System.Threading;

namespace PathSystem.Entity
{
    public class ConfigService
    {
        private Random _random = new();

        public bool IsAutoConfig()
        {
            Console.WriteLine("Konfiguracja automatyczna nastąpi w ciągu 5 sekund.\nWcisnij jakikolwiek klawisz aby konfigurować ręcznie...");

            bool autoConfig = true;
            DateTime timeToWait = DateTime.Now.AddSeconds(5);

            while (DateTime.Now < timeToWait)
            {
                if (Console.KeyAvailable)
                {
                    autoConfig = false;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            return autoConfig;
        }

        public ActiveEntityModel CreateEntity(bool autoConfig)
        {
            Console.Clear();

            if (autoConfig)
                return RandomEntity();
            else
            {
                string entityName = null;
                while (string.IsNullOrWhiteSpace(entityName)) 
                {
                    Console.Clear();
                    Console.WriteLine("Podaj nazwię jednostki: ");
                    entityName = Console.ReadLine();
                }

                int entitySpeed = 0;
                while (entitySpeed <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj szybkość jednostki (ilość milisekund / 1 pixel mapy): ");
                    string textSpeed = Console.ReadLine();

                    if (!int.TryParse(textSpeed, out entitySpeed))
                        continue;
                }

                Console.Clear();

                return new ActiveEntityModel() 
                {
                    Guid = Guid.NewGuid(),
                    Name = entityName,
                    Speed = entitySpeed
                };
            }
        }

        private ActiveEntityModel RandomEntity()
        {
            return new ActiveEntityModel()
            {
                Guid = Guid.NewGuid(),
                Name = "Entity" + _random.Next(1, 1000),
                Speed = _random.Next(100, 2000)
            };
        }
    }
}
