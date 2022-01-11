using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EFEntityRepository : IEntityRepository
    {
        private readonly EFContext _context;

        public EFEntityRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<EntityModel> AddEntity(EntityModel entityModel)
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

        public async Task<IEnumerable<EntityModel>> GetEntities() => await _context.Entities.ToArrayAsync();

        public async Task<EntityModel> GetEntity(int entityId) => await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityId);

        public async Task<EntityModel> GetEntity(Guid entityGuid) => await _context.Entities.FirstOrDefaultAsync(e => e.Guid == entityGuid);

        public async Task<EntityModel> UpdateEntity(EntityModel entityModel)
        {
            var result = await _context.Entities.FirstOrDefaultAsync(e => e.Id == entityModel.Id);

            if (result != null)
            {
                result.Guid = entityModel.Guid;
                result.Name = entityModel.Name;
                result.Speed = entityModel.Speed;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
