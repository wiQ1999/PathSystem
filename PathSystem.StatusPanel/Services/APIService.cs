using PathSystem.Models;
using RestSharp;
using System.Collections.Generic;

namespace PathSystem.StatusPanel.Services
{
    public class APIService
    {
        const string URL = "https://localhost:44303/api/statuspanel/";

        public RestClient Client { get; } = new(URL);

        public IEnumerable<MapPositionModel> GetMap()
        {
            var request = new RestRequest("map", Method.Get);

            return Client.GetAsync<IEnumerable<MapPositionModel>>(request).Result;
        }

        public IEnumerable<EntityModel> GetEntities()
        {
            var request = new RestRequest("entities", Method.Get);

            return Client.GetAsync<IEnumerable<EntityModel>>(request).Result;
        }

        public IEnumerable<PathModel> GetPaths()
        {
            var request = new RestRequest("paths", Method.Get);

            return Client.GetAsync<IEnumerable<PathModel>>(request).Result;
        }
    }
}
