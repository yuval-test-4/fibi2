using EventsApi.APIs;

namespace EventsApi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEventsService, EventsService>();
    }
}
