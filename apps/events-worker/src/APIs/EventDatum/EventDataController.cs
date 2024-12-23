using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[ApiController()]
public class EventDataController : EventDataControllerBase
{
    public EventDataController(IEventDataService service)
        : base(service) { }
}
