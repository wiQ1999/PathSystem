using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using PathSystem.Models.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class PathRepository : IPathRepository
    {
        private readonly EFContext _context;

        public PathRepository(EFContext context)
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

        public async Task<Path> AddPath(Path pathModel)
        {
            var result = await _context.Paths.AddAsync(pathModel);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Path> GetLastPath(Entity entityModel) =>
            await _context.Paths.Include(p => p.Entity).Include(p => p.PathPositions).FirstOrDefaultAsync(p => p.Entity.Id == entityModel.Id);

        public async Task<Path> GetPath(int pathId) => 
            await _context.Paths.Include(p => p.Entity).Include(p => p.PathPositions).FirstOrDefaultAsync(p => p.Id == pathId);

        public async Task<IEnumerable<Path>> GetPaths(bool? finished = null) 
        { 
            var result = _context.Paths.Include(p => p.Entity).Include(p => p.PathPositions);

            if (finished != null)
                return await result.Where(p => p.IsFinished == (bool)finished).ToArrayAsync();

            return await result.ToArrayAsync();
        }

        public async Task<IEnumerable<Path>> GetPaths(Entity entityModel, bool? finished = null)
        {
            var result = _context.Paths.Include(p => p.Entity).Include(p => p.PathPositions).Where(p => p.Entity.Id == entityModel.Id);

            if (finished != null)
                return await result.Where(p => p.IsFinished == (bool)finished).ToArrayAsync();

            return await result.ToArrayAsync();
        }

        public async Task<Path> UpdatePath(Path pathModel)
        {
            var result = await _context.Paths.FirstOrDefaultAsync(p => p.Id == pathModel.Id);

            if (result != null)
            {
                result.Entity = pathModel.Entity;
                result.PathPositions = pathModel.PathPositions;
                result.IsFinished = pathModel.IsFinished;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Path> UpdatePathFinished(Path pathModel, bool isFinished)
        {
            var result = await _context.Paths.FirstOrDefaultAsync(p => p.Id == pathModel.Id);

            if (result != null)
            {
                result.IsFinished = isFinished;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
