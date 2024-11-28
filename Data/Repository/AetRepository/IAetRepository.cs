using Rumo.Models;

namespace Rumo.Data.Repository.AetRepository;

public interface IAetRepository
{
    Task<List<Aet>> GetAllAsync();
    Task<List<Aet>> GetAllOrderBy(String id);
    Task<Aet?> GetById(Guid id);
    Task<List<Verser>> GetVersersByAet(Guid id);
    Task AddAet(Aet aet);
    Task Update(Aet aet);
    Task Delete(Aet aet);
    bool AetExists(Guid id);
}