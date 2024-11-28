using Microsoft.EntityFrameworkCore;
using Rumo.Data.Repository.VehicleRepository;
using Rumo.Models;

namespace Rumo.Data.Repository.VehicleRepository;

public class VehicleRepository(Context context) : IVechicleRepository
{
    public async Task Create(Vehicle vehicle)
    {   
        context.Vehicles.Add(vehicle);
        await context.SaveChangesAsync();
    }

    public async Task<Vehicle> GetById(string id)
    {
       return await context.Vehicles.FirstOrDefaultAsync(m => m.Plate.Contains(id) || m.Renavam.Contains(id) || m.Chassis.Contains(id));;
    }

    public async Task Delete(Vehicle vehicle)
    {
        context.Vehicles.Remove(vehicle);
        await context.SaveChangesAsync();
    }

    public async Task Edit(Vehicle vehicle)
    {
        context.Update(vehicle);
        await context.SaveChangesAsync();
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await context.Vehicles.OrderBy(v => v.Plate).ToListAsync();
    }

    public async Task<List<Vehicle>> Index(string month)
    {
        return month switch
        {
            "Julho" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("1") || v.Plate.EndsWith("2"))).ToListAsync(),
            "Agosto" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("3") || v.Plate.EndsWith("4"))).ToListAsync(),
            "Setembro" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("5") || v.Plate.EndsWith("6")) || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("1") || v.Plate.EndsWith("2"))).ToListAsync(),
            "Outubro" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("7") || v.Plate.EndsWith("8")) || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("3") || v.Plate.EndsWith("4") || v.Plate.EndsWith("5"))).ToListAsync(),
            "Novembro" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && v.Plate.EndsWith("9") || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("6") || v.Plate.EndsWith("7") || v.Plate.EndsWith("8"))).ToListAsync(),
            "Dezembro" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && v.Plate.EndsWith("0") || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("9") || v.Plate.EndsWith("8"))).ToListAsync(),
            "Ativo" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => v.Situation.Contains("Ativo")).ToListAsync(),
            "Desativado" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => v.Situation.Contains("Desativado")).ToListAsync(),
            "Vencidos" => await context.Vehicles.OrderBy(v => v.Plate).Where(v => v.DuoDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now).ToListAsync(),
            _ => await context.Vehicles.OrderBy(v => v.Plate).Where(v => v.DuoDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now).ToListAsync(),
        };
    }

    public bool VehicleExists(Vehicle id)
    {
        return context.Vehicles.Any(e => e.Plate.Contains(id.Plate) || e.Renavam.Contains(id.Renavam) || e.Chassis.Contains(id.Chassis));
    }

}