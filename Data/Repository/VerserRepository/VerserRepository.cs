using Microsoft.EntityFrameworkCore;
using Rumo.Models;

namespace Rumo.Data.Repository.VerserReposiroey;
public class VerserRepository(Context context) : IVerserRepository
{
    public async Task Create(Verser verser)
    {
        context.Versers.Add(verser);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Verser verser)
    {
        context.Versers.Remove(verser);
        await context.SaveChangesAsync();
    }

    public async Task<List<Verser>> GetAllAsync()
    {
        return await context.Versers.OrderBy(a => a.VehicleId).ToListAsync();
    }

    public async Task<Verser> GetVerserbyId(Guid id)
    {
        return await context.Versers.FindAsync(id);
    }

    public async Task update(Verser verser)
    {
        context.Versers.Update(verser);
        await context.SaveChangesAsync();
    }

    public bool VerserExists(Guid id)
    {
        return context.Versers.Any(e => e.id == id);;
    }
}