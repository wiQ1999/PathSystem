using PathSystem.Models;
using PathSystem.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathSystem.Database.Interfaces
{
    public interface IPathRepository
    {
        Task<Path> AddPath(Path pathModel);
        Task<IEnumerable<Path>> GetPaths(bool? finished);
        Task<IEnumerable<Path>> GetPaths(Entity entityModel, bool? finished);
        Task<Path> GetPath(int pathId);
        Task<Path> GetLastPath(Entity entityModel);
        Task<Path> UpdatePath(Path pathModel);
        Task<Path> UpdatePathFinished(Path pathModel, bool isFinished);
        Task DeletePath(int pathId);
    }
}
