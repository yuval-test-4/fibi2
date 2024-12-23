using Microsoft.EntityFrameworkCore;

namespace EventsApi.Infrastructure;

public class EventsApiDbContext : DbContext
{
    public EventsApiDbContext(DbContextOptions<EventsApiDbContext> options)
        : base(options) { }
}
