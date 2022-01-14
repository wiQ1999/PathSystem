using PathSystem.Database.Repositories;
using PathSystem.Models;
using PathSystem.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.API.MapService
{
    public sealed class MapInstance
    {
        private static readonly Task<MapInstance> _instance = InitializeInstance();

        public readonly bool[,] MapArray;

        public readonly IEnumerable<MapPositionModel> MapModel;

        private MapInstance(bool[,] mapArray, IEnumerable<MapPositionModel> mapModel)
        {
            MapArray = mapArray;
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
            EFMapRepository mapRepository = new(new Database.EFContext());
            IEnumerable<MapPositionModel> mapModel = await mapRepository.GetMap();

            return new MapInstance(MapHelper.ConvertToMapArray(mapModel), mapModel);
        }
    }
}
