using Rumo.Models;

namespace Rumo.Data.Repository.VehicleRepository;

public interface IVechicleRepository
{
    Task<List<Vehicle>> GetAllAsync();
    Task<List<Vehicle>> Index(string month);
    Task Create(Vehicle vehicle);
    Task Edit(Vehicle vehicle);
    Task<Vehicle> GetById(string id);
    Task Delete(Vehicle vehicle);
    bool VehicleExists(Vehicle id);
}
