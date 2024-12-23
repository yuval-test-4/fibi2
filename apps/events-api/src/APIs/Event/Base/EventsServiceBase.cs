using EventsApi.APIs;
using EventsApi.APIs.Dtos;
using EventsApi.Infrastructure;
using EventsApi.Infrastructure.Models;

namespace EventsApi.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly EventsApiDbContext _context;

    public EventsServiceBase(EventsApiDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateEvent(EventCreateInput eventCreateInputDto)
    {
        throw new NotImplementedException();
    }
}
