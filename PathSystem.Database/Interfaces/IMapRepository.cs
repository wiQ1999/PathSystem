using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IMapRepository
    {
        Task<IEnumerable<MapPosition>> AddMap(IEnumerable<MapPosition> mapModels);
        Task<IEnumerable<MapPosition>> GetMap();
    }
}
