using EventsApi.Infrastructure;

namespace EventsApi.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(EventsApiDbContext context)
        : base(context) { }
}
