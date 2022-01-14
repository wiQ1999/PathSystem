using PathSystem.Models;
using PathSystem.Models.Tables;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PathSystem.StatusPanel.Services
{
    public class APIService
    {
        const string URL = "https://localhost:44303/api/statuspanel/";

        public RestClient Client { get; } = new(URL);

        public IEnumerable<MapPosition> GetMap()
        {
            var request = new RestRequest("map", Method.Get);

            return Client.GetAsync<IEnumerable<MapPosition>>(request).Result;
        }

        public IEnumerable<Entity> GetEntities()
        {
            var request = new RestRequest("entities", Method.Get);

            return Client.GetAsync<IEnumerable<Entity>>(request).Result;
        }

        public IEnumerable<Path> GetPaths()
        {
            var request = new RestRequest("paths", Method.Get);

            return Client.GetAsync<IEnumerable<Path>>(request).Result;
        }
    }
}
