using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EFEntityPositionRepository : IEntityPositionRepository
    {
        private readonly EFContext _context;

        public EFEntityPositionRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<EntityPositionModel> AddEntityPosition(EntityPositionModel entityPositionModel)
        {
            var result = await _context.EntitiesPosition.AddAsync(entityPositionModel);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<EntityPositionModel>> GetEntitiesPosition(bool lastActivate = true)
        {
            var result = _context.EntitiesPosition;

            if (lastActivate)
                result.GroupBy(ep => ep.CreatedDateTime);

            return await result.ToArrayAsync();
        }

        public async Task<EntityPositionModel> GetEntityPosition(int entityPositionId) => await _context.EntitiesPosition.FirstOrDefaultAsync(e => e.Id == entityPositionId);

        public async Task<EntityPositionModel> GetEntityPosition(EntityModel entityModel) => await _context.EntitiesPosition.FirstOrDefaultAsync(e => e.Entity == entityModel);
    }
}