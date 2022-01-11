using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class EFMapRepository : IMapRepository
    {
        private readonly EFContext _context;

        public EFMapRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MapPositionModel>> AddMap(IEnumerable<MapPositionModel> mapModels) // być może zapisywać po kolei każdą pozycję i zapisywać context
        {
            return null;

            //Task<IEnumerable<MapPositionModel>> result = new Task<IEnumerable<MapPositionModel>>()

            //foreach (MapPositionModel mapModel in mapModels)
            //{
            //    var result = await _context.Map.AddAsync(mapModel);
            //    await _context.SaveChangesAsync();
            //}
            

            //return result.Entity;
        }

        public async Task<IEnumerable<MapPositionModel>> GetMap() => await _context.Map.ToArrayAsync();

        public async Task<IEnumerable<MapPositionModel>> UpdateMap(IEnumerable<MapPositionModel> mapModels)
        {
            _context.Map.RemoveRange(GetMap().Result);

            await _context.SaveChangesAsync();

            return await AddMap(mapModels);
        }
    }
}
