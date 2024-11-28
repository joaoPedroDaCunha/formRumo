using Rumo.Data.Repository.VehicleRepository;

namespace Rumo.Data.Configure;

public static class RegisterService
{
    public static void AddConfigureData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IVechicleRepository,VehicleRepository>();
    }
}