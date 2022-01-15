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

            return Client.ExecuteGetAsync<IEnumerable<MapPosition>>(request).Result.Data;
        }

        public IEnumerable<Entity> GetEntities()
        {
            var request = new RestRequest("entities", Method.Get);

            return Client.ExecuteGetAsync<IEnumerable<Entity>>(request).Result.Data;
        }

        public EntityPosition GetEntityLastPosition(Entity entity)
        {
            var request = new RestRequest("position", Method.Get);

            request.AddBody(entity);

            return Client.ExecuteGetAsync<EntityPosition>(request).Result.Data;
        }

        public IEnumerable<Path> GetPaths()
        {
            var request = new RestRequest("paths", Method.Get);

            return Client.ExecuteGetAsync<IEnumerable<Path>>(request).Result.Data;
        }
    }
}
