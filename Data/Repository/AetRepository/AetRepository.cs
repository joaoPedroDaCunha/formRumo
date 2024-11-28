using Microsoft.EntityFrameworkCore;
using Rumo.Models;

namespace Rumo.Data.Repository.AetRepository;

public class AetRepository(Context context) : IAetRepository
{
    public async Task AddAet(Aet aet)
    {
        aet.id = Guid.NewGuid();
        context.Aets.Add(aet);
        await context.SaveChangesAsync();
    }

    public bool AetExists(Guid id)
    {
        return context.Aets.Any(e => e.id == id);
    }

    public async Task Delete(Aet aet)
    {
        context.Aets.Remove(aet);
        await context.SaveChangesAsync();
    }

    public async Task<List<Aet>> GetAllAsync()
    {
        return await context.Aets.OrderBy(a => a.VehicleId).ToListAsync();
    }

    public async Task<List<Aet>> GetAllOrderBy(string id)
    {
        return id switch
            {
                "Janeiro" => await context.Aets.Where(a => a.Date.Month == 1).OrderBy(a => a.VehicleId).ToListAsync(),
                "Fevereiro" => await context.Aets.Where(a => a.Date.Month == 2).OrderBy(a => a.VehicleId).ToListAsync(),
                "Março" => await context.Aets.Where(a => a.Date.Month == 3).OrderBy(a => a.VehicleId).ToListAsync(),
                "Abril" => await context.Aets.Where(a => a.Date.Month == 4).OrderBy(a => a.VehicleId).ToListAsync(),
                "Maio" => await context.Aets.Where(a => a.Date.Month == 5).OrderBy(a => a.VehicleId).ToListAsync(),
                "Junho" => await context.Aets.Where(a => a.Date.Month == 6).OrderBy(a => a.VehicleId).ToListAsync(),
                "Julho" => await context.Aets.Where(a => a.Date.Month == 7).OrderBy(a => a.VehicleId).ToListAsync(),
                "Agosto" => await context.Aets.Where(a => a.Date.Month == 8).OrderBy(a => a.VehicleId).ToListAsync(),
                "Setembro" => await context.Aets.Where(a => a.Date.Month == 9).OrderBy(a => a.VehicleId).ToListAsync(),
                "Outubro" => await context.Aets.Where(a => a.Date.Month == 10).OrderBy(a => a.VehicleId).ToListAsync(),
                "Novembro" => await context.Aets.Where(a => a.Date.Month == 11).OrderBy(a => a.VehicleId).ToListAsync(),
                "Dezembro" => await context.Aets.Where(a => a.Date.Month == 12).OrderBy(a => a.VehicleId).ToListAsync(),
                "Ativos" => await context.Aets.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) > DateTime.Now).ToListAsync(),
                "Vencidos" =>await context.Aets.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) < DateTime.Now).ToListAsync(),
                _ => await context.Aets.Where(a => a.Vehicle.Plate.Equals(id)).ToListAsync(),
            };
    }

    public async Task<Aet?> GetById(Guid id)
    {
        return await context.Aets.Include(a => a.Vehicle).FirstOrDefaultAsync(m => m.id == id);
    }


    public async Task<List<Verser>> GetVersersByAet(Guid id)
    {
        return await context.Versers.Where(a => a.AetId.Equals(id)).Include(a => a.Vehicle).ToListAsync();
    }

    public async Task Update(Aet aet)
    {
        context.Aets.Update(aet);
        await context.SaveChangesAsync();
    }
}