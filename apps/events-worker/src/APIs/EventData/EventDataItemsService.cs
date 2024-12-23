using EventsWorker.Infrastructure;

namespace EventsWorker.APIs;

public class EventDataItemsService : EventDataItemsServiceBase
{
    public EventDataItemsService(EventsWorkerDbContext context)
        : base(context) { }
}
