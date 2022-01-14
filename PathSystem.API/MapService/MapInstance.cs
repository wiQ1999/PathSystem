using PathSystem.Database.Repositories;
using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.API.MapService
{
    public sealed class MapInstance
    {
        private static readonly Task<MapInstance> _instance = InitializeInstance();

        public readonly IEnumerable<MapPosition> MapModel;

        private MapInstance(IEnumerable<MapPosition> mapModel)
        {
            MapModel = mapModel;
        }

        public static async Task<MapInstance> GetInstance()
        {
            if (_instance == null)
                await InitializeInstance();

            return await _instance;
        }

        private static async Task<MapInstance> InitializeInstance()
        {
            MapRepository mapRepository = new(new Database.EFContext());
            IEnumerable<MapPosition> mapModel = await mapRepository.GetMap();

            return new MapInstance(mapModel);
        }
    }
}
