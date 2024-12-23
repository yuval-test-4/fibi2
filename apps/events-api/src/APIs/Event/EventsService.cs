using EventsApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(EventsApiDbContext context)
        : base(context) { }
}
