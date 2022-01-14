using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using PathSystem.Models.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EntityPositionRepository : IEntityPositionRepository
    {
        private readonly EFContext _context;

        public EntityPositionRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<EntityPosition> AddEntityPosition(EntityPosition entityPositionModel)
        {
            var result = await _context.EntitiesPosition.AddAsync(entityPositionModel);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<EntityPosition>> GetEntitiesPosition(bool lastActivate = true)
        {
            var result = _context.EntitiesPosition;

            if (lastActivate)
                result.GroupBy(ep => ep.CreatedDateTime);

            return await result.ToArrayAsync();
        }

        public async Task<EntityPosition> GetEntityPosition(int entityPositionId) => 
            await _context.EntitiesPosition.FirstOrDefaultAsync(e => e.Id == entityPositionId);

        public async Task<EntityPosition> GetEntityPosition(Entity entityModel) => 
            await _context.EntitiesPosition.FirstOrDefaultAsync(e => e.Entity.Id == entityModel.Id);
    }
}