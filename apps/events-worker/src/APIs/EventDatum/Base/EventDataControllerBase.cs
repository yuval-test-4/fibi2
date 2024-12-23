using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventDataControllerBase : ControllerBase
{
    protected readonly IEventDataService _service;

    public EventDataControllerBase(IEventDataService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one EventData
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<EventDatum>> CreateEventDatum(EventDatumCreateInput input)
    {
        var eventDatum = await _service.CreateEventDatum(input);

        return CreatedAtAction(nameof(EventDatum), new { id = eventDatum.Id }, eventDatum);
    }

    /// <summary>
    /// Delete one EventData
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEventDatum(
        [FromRoute()] EventDatumWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEventDatum(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many EventDataItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<EventDatum>>> EventData(
        [FromQuery()] EventDatumFindManyArgs filter
    )
    {
        return Ok(await _service.EventData(filter));
    }

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventDataMeta(
        [FromQuery()] EventDatumFindManyArgs filter
    )
    {
        return Ok(await _service.EventDataMeta(filter));
    }

    /// <summary>
    /// Get one EventData
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<EventDatum>> EventDatum(
        [FromRoute()] EventDatumWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.EventDatum(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one EventData
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEventDatum(
        [FromRoute()] EventDatumWhereUniqueInput uniqueId,
        [FromQuery()] EventDatumUpdateInput eventDatumUpdateDto
    )
    {
        try
        {
            await _service.UpdateEventDatum(uniqueId, eventDatumUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Group record for EventData
    /// </summary>
    [HttpGet("{Id}/group")]
    public async Task<ActionResult<List<Group>>> GetGroup(
        [FromRoute()] EventDatumWhereUniqueInput uniqueId
    )
    {
        var group = await _service.GetGroup(uniqueId);
        return Ok(group);
    }
}
