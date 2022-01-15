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

        public async Task<IEnumerable<EntityPosition>> GetEntitiesPosition(bool lastActivate = false)
        {
            var result = _context.EntitiesPosition.Include(e => e.Entity);

            if (lastActivate)
                return result.GroupBy(e => e.Entity.Id).Select(e => e.OrderByDescending(eg => eg.CreatedDateTime).FirstOrDefault()).ToArray();

            return await result.ToArrayAsync();
        }

        public async Task<EntityPosition> GetEntityPosition(Entity entityModel) => 
            await _context.EntitiesPosition.Include(e => e.Entity).OrderByDescending(e => e.CreatedDateTime).FirstOrDefaultAsync(e => e.Entity.Id == entityModel.Id);
    }
}