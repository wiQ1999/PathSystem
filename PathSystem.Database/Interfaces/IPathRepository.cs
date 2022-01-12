using PathSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IPathRepository
    {
        Task<IEnumerable<PathModel>> GetPaths(bool? finished);
        Task<IEnumerable<PathModel>> GetPaths(EntityModel entityModel);
        Task<PathModel> GetPath(int pathId);
        Task<PathModel> GetLastPath(EntityModel entityModel);
        Task<PathModel> UpdatePath(PathModel pathModel);
        Task DeletePath(int pathId);
    }
}
