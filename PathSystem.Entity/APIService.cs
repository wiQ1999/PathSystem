using PathSystem.Models;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PathSystem.Entity
{
    public class APIService
    {
        const string URL = "https://localhost:44303/api/entitypath/";

        public RestClient Client { get; } = new(URL);

        public RestResponse<ActiveEntityPositionModel> InitializeEntity(ActiveEntityModel entity)
        {
            var request = new RestRequest("initialize", Method.Post);

            request.AddBody(entity);

            return Client.ExecutePostAsync<ActiveEntityPositionModel>(request).Result;
        }

        public IEnumerable<MapPosition> GetMap()
        {
            var request = new RestRequest("map", Method.Get);

            return Client.ExecuteGetAsync<IEnumerable<MapPosition>>(request).Result.Data;
        }

        public RestResponse<IEnumerable<PathPosition>> GetPath(ActiveEntityPositionPathModel entityPositionPath)
        {
            var request = new RestRequest("path", Method.Post);

            request.AddBody(entityPositionPath);

            return Client.ExecutePostAsync<IEnumerable<PathPosition>>(request).Result;
        }

        public RestResponse<ActiveEntityPositionModel> PutPosition(ActiveEntityPositionModel entityPosition)
        {
            var request = new RestRequest("position", Method.Put);

            request.AddBody(entityPosition);

            return Client.ExecutePutAsync<ActiveEntityPositionModel>(request).Result;
        }
    }
}
