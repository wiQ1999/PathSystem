using PathSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IEntityRepository
    {
        Task<Entity> AddEntity(Entity entityModel);
        Task<IEnumerable<Entity>> GetEntities();
        Task<Entity> GetEntity(int entityId);
        Task<Entity> GetEntity(Guid entityGuid);
        Task<Entity> UpdateEntity(Entity entityModel);
        Task<Entity> UpdateEntityActivity(Entity entityModel);
        Task DeleteEntity(int entityId);
        Task DeleteEntity(Guid entityGuid);
    }
}
