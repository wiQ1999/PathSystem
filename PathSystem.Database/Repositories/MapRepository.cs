using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Interfaces;
using PathSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystem.Database.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly EFContext _context;

        public MapRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MapPosition>> AddMap(IEnumerable<MapPosition> mapModels)
        {
            _context.Map.RemoveRange(GetMap().Result);

            await _context.SaveChangesAsync();

            return await Task.Run(() =>
            {
                List<MapPosition> result = new(mapModels.Count());

                foreach (MapPosition mapModel in mapModels)
                {
                    var addedModel = _context.Map.Add(mapModel);
                    _context.SaveChangesAsync();

                    if (addedModel != null && addedModel.Entity != null)
                        result.Add(addedModel.Entity);
                }

                return result;
            });
        }

        public async Task<IEnumerable<MapPosition>> GetMap() => 
            await _context.Map.ToArrayAsync();
    }
}
