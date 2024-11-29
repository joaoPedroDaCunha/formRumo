using Rumo.Data.Repository.AetRepository;
using Rumo.Data.Repository.VehicleRepository;
using Rumo.Data.Repository.VerserReposiroey;

namespace Rumo.Data.Configure;

public static class RegisterService
{
    public static void AddConfigureData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IVechicleRepository,VehicleRepository>();
        services.AddTransient<IAetRepository,AetRepository>();
        services.AddTransient<IVerserRepository,VerserRepository>();
    }
}