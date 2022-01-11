using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IEntityRepository
    {
        Task<EntityModel> AddEntity(EntityModel entityModel);
        Task<IEnumerable<EntityModel>> GetEntities();
        Task<EntityModel> GetEntity(int entityId);
        Task<EntityModel> GetEntity(Guid entityGuid);
        Task<EntityModel> UpdateEntity(EntityModel entityModel);
        Task DeleteEntity(int entityId);
        Task DeleteEntity(Guid entityGuid);
    }
}
