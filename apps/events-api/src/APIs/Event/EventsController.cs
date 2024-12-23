using EventsApi.APIs;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
