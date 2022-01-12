using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IEntityPositionRepository
    {
        Task<EntityPositionModel> AddEntityPosition(EntityPositionModel entityPositionModel);
        Task<IEnumerable<EntityPositionModel>> GetEntitiesPosition(bool lastActivate);
        Task<EntityPositionModel> GetEntityPosition(int entityPositionId);
        Task<EntityPositionModel> GetEntityPosition(EntityModel entityModel);
    }
}
