using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EFPathRepository : IPathRepository
    {
        private readonly EFContext _context;

        public EFPathRepository(EFContext context)
        {
            _context = context;
        }

        public async Task DeletePath(int pathId)
        {
            var result = await _context.Paths.FirstOrDefaultAsync(e => e.Id == pathId);

            if (result != null)
            {
                _context.Paths.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PathModel> GetLastPath(EntityModel entityModel) => await _context.Paths.FirstOrDefaultAsync(p => p.Entity == entityModel);

        public async Task<PathModel> GetPath(int pathId) => await _context.Paths.FirstOrDefaultAsync(e => e.Id == pathId);

        public async Task<IEnumerable<PathModel>> GetPaths(bool? finished = null) 
        { 
            var result = _context.Paths;

            if (finished != null)
                return await result.Where(p => p.IsFinished == (bool)finished).ToArrayAsync();

            return await result.ToArrayAsync();
        }

        public async Task<IEnumerable<PathModel>> GetPaths(EntityModel entityModel) => await _context.Paths.Where(p => p.Entity == entityModel).ToArrayAsync();

        public async Task<PathModel> UpdatePath(PathModel pathModel)
        {
            var result = await _context.Paths.FirstOrDefaultAsync(e => e.Id == pathModel.Id);

            if (result != null)
            {
                result.Entity = pathModel.Entity;
                result.Points = pathModel.Points;
                result.IsFinished = pathModel.IsFinished;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
