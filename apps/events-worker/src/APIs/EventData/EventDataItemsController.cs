using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[ApiController()]
public class EventDataItemsController : EventDataItemsControllerBase
{
    public EventDataItemsController(IEventDataItemsService service)
        : base(service) { }
}
