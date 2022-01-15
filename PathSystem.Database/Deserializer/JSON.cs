using Newtonsoft.Json;
using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathSystem.Database.Deserializer
{
    public class JSON
    {
        private string PROFILESPATH = Environment.CurrentDirectory + "\\Resources\\Map02.json";

        //private readonly JsonSerializer _json = new JsonSerializer();

        public List<MapPosition> Deserialize()
        {
            List<MapPosition> result = new();

            if (!File.Exists(PROFILESPATH))
                return result;

            using (TextReader reader = new StreamReader(PROFILESPATH))
            {
                string jsonString = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<MapPosition>>(jsonString);
            }

            return result;
        }
    }
}
