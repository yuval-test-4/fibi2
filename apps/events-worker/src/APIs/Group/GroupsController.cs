using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[ApiController()]
public class GroupsController : GroupsControllerBase
{
    public GroupsController(IGroupsService service)
        : base(service) { }
}
