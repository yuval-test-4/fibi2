using EventsWorker.Infrastructure;

namespace EventsWorker.APIs;

public class EventDataService : EventDataServiceBase
{
    public EventDataService(EventsWorkerDbContext context)
        : base(context) { }
}
