using EventsWorker.Infrastructure;

namespace EventsWorker.APIs;

public class GroupsService : GroupsServiceBase
{
    public GroupsService(EventsWorkerDbContext context)
        : base(context) { }
}
