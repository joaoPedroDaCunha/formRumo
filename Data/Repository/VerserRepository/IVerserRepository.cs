using System.Runtime.Intrinsics.Arm;
using Rumo.Models;

namespace Rumo.Data.Repository.VerserReposiroey;

public interface IVerserRepository
{
    Task<List<Verser>> GetAllAsync();
    Task<Verser> GetVerserbyId(Guid id);
    Task Create(Verser verser);
    Task Delete(Verser verser);
    Task update(Verser verser);
    bool VerserExists(Guid id);
}