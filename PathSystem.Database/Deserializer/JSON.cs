using Newtonsoft.Json;
using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathSystem.Database.Deserializer
{
    public class JSON
    {
        private string PROFILESPATH = Environment.CurrentDirectory + "\\Resources\\Map01.json";

        //private readonly JsonSerializer _json = new JsonSerializer();

        public List<MapPositionModel> Deserialize()
        {
            List<MapPositionModel> result = new List<MapPositionModel>();

            if (!File.Exists(PROFILESPATH))
                return result;

            using (TextReader reader = new StreamReader(PROFILESPATH))
            {
                string jsonString = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<MapPositionModel>>(jsonString);
            }

            return result;
        }
    }
}
