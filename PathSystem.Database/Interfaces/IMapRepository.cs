using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IMapRepository
    {
        Task<IEnumerable<MapPositionModel>> AddMap(IEnumerable<MapPositionModel> mapModels);
        Task<IEnumerable<MapPositionModel>> GetMap();
    }
}
