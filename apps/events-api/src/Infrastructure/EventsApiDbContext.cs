using EventsApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Infrastructure;

public class EventsApiDbContext : DbContext
{
    public EventsApiDbContext(DbContextOptions<EventsApiDbContext> options)
        : base(options) { }

    public DbSet<UserDbModel> Users { get; set; }
}
