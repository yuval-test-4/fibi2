using EventsApi.APIs.Dtos;

namespace EventsApi.APIs;

public interface IEventsService
{
    public Task<string> CreateEvent(EventCreateInput eventCreateInputDto);
}
