using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventDataItemsControllerBase : ControllerBase
{
    protected readonly IEventDataItemsService _service;

    public EventDataItemsControllerBase(IEventDataItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one EventData
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<EventData>> CreateEventData(EventDataCreateInput input)
    {
        var eventData = await _service.CreateEventData(input);

        return CreatedAtAction(nameof(EventData), new { id = eventData.Id }, eventData);
    }

    /// <summary>
    /// Delete one EventData
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEventData(
        [FromRoute()] EventDataWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEventData(uniqueId);
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
    public async Task<ActionResult<List<EventData>>> EventDataItems(
        [FromQuery()] EventDataFindManyArgs filter
    )
    {
        return Ok(await _service.EventDataItems(filter));
    }

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventDataItemsMeta(
        [FromQuery()] EventDataFindManyArgs filter
    )
    {
        return Ok(await _service.EventDataItemsMeta(filter));
    }

    /// <summary>
    /// Get one EventData
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<EventData>> EventData(
        [FromRoute()] EventDataWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.EventData(uniqueId);
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
    public async Task<ActionResult> UpdateEventData(
        [FromRoute()] EventDataWhereUniqueInput uniqueId,
        [FromQuery()] EventDataUpdateInput eventDataUpdateDto
    )
    {
        try
        {
            await _service.UpdateEventData(uniqueId, eventDataUpdateDto);
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
        [FromRoute()] EventDataWhereUniqueInput uniqueId
    )
    {
        var group = await _service.GetGroup(uniqueId);
        return Ok(group);
    }
}
