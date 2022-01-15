using PathSystem.Models;
using PathSystem.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IEntityPositionRepository
    {
        Task<EntityPosition> AddEntityPosition(EntityPosition entityPositionModel);
        Task<IEnumerable<EntityPosition>> GetEntitiesPosition(bool lastActivate);
        Task<EntityPosition> GetEntityPosition(Entity entityModel);
    }
}
