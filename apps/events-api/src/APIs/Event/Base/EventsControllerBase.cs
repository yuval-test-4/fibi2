using EventsApi.APIs;
using EventsApi.APIs.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;

    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    [HttpPost("")]
    public async Task<string> CreateEvent([FromBody()] EventCreateInput eventCreateInputDto)
    {
        return await _service.CreateEvent(eventCreateInputDto);
    }
}
