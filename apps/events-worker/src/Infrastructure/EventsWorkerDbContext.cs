using EventsWorker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWorker.Infrastructure;

public class EventsWorkerDbContext : DbContext
{
    public EventsWorkerDbContext(DbContextOptions<EventsWorkerDbContext> options)
        : base(options) { }

    public DbSet<EventDataDbModel> EventDataItems { get; set; }

    public DbSet<GroupDbModel> Groups { get; set; }
}
