using Microsoft.EntityFrameworkCore;
using TestProject.App.CometManagement;
using TestProject.Infrastructure;
using TestProject.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IDataNasaServices, DataNasaServices>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://data.nasa.gov");
        });
        services.AddDbContext<CometDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("connectionString")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        return services.AddScoped<IWorkService, WorkService>();
    }
}