using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly EFContext _context;

        public EntityRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<Entity> AddEntity(Entity entityModel)
        {
            var result = await _context.Entities.AddAsync(entityModel);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteEntity(int entityId)
        {
            var result = await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityId);

            if (result != null)
            {
                _context.Entities.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEntity(Guid entityGuid)
        {
            var result = await _context.Entities.FirstOrDefaultAsync(e => e.Guid == entityGuid);

            if (result != null)
            {
                _context.Entities.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entity>> GetEntities() => 
            await _context.Entities.ToArrayAsync();

        public async Task<Entity> GetEntity(int entityId) => 
            await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityId);

        public async Task<Entity> GetEntity(Guid entityGuid) => 
            await _context.Entities.FirstOrDefaultAsync(e => e.Guid == entityGuid);

        public async Task<Entity> UpdateEntity(Entity entityModel)
        {
            var result = await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityModel.Id);

            if (result != null)
            {
                result.Guid = entityModel.Guid;
                result.Name = entityModel.Name;
                result.Speed = entityModel.Speed;
                result.IsActive = entityModel.IsActive;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Entity> UpdateEntityActivity(Entity entityModel)
        {
            var result = await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityModel.Id);

            if (result != null)
            {
                result.IsActive = entityModel.IsActive;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
