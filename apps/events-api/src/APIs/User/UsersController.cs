using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
