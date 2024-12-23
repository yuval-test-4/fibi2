using EventsWorker.APIs;

namespace EventsWorker;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEventDataService, EventDataService>();
        services.AddScoped<IGroupsService, GroupsService>();
    }
}
