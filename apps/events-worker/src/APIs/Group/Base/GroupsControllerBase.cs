using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventsWorker.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GroupsControllerBase : ControllerBase
{
    protected readonly IGroupsService _service;

    public GroupsControllerBase(IGroupsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Group
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Group>> CreateGroup(GroupCreateInput input)
    {
        var group = await _service.CreateGroup(input);

        return CreatedAtAction(nameof(Group), new { id = group.Id }, group);
    }

    /// <summary>
    /// Delete one Group
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteGroup([FromRoute()] GroupWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGroup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Groups
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Group>>> Groups([FromQuery()] GroupFindManyArgs filter)
    {
        return Ok(await _service.Groups(filter));
    }

    /// <summary>
    /// Meta data about Group records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GroupsMeta([FromQuery()] GroupFindManyArgs filter)
    {
        return Ok(await _service.GroupsMeta(filter));
    }

    /// <summary>
    /// Get one Group
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Group>> Group([FromRoute()] GroupWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Group(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Group
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateGroup(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] GroupUpdateInput groupUpdateDto
    )
    {
        try
        {
            await _service.UpdateGroup(uniqueId, groupUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple EventData records to Group
    /// </summary>
    [HttpPost("{Id}/eventData")]
    public async Task<ActionResult> ConnectEventData(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] EventDatumWhereUniqueInput[] eventDataId
    )
    {
        try
        {
            await _service.ConnectEventData(uniqueId, eventDataId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple EventData records from Group
    /// </summary>
    [HttpDelete("{Id}/eventData")]
    public async Task<ActionResult> DisconnectEventData(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromBody()] EventDatumWhereUniqueInput[] eventDataId
    )
    {
        try
        {
            await _service.DisconnectEventData(uniqueId, eventDataId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple EventData records for Group
    /// </summary>
    [HttpGet("{Id}/eventData")]
    public async Task<ActionResult<List<EventDatum>>> FindEventData(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] EventDatumFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEventData(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple EventData records for Group
    /// </summary>
    [HttpPatch("{Id}/eventData")]
    public async Task<ActionResult> UpdateEventData(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromBody()] EventDatumWhereUniqueInput[] eventDataId
    )
    {
        try
        {
            await _service.UpdateEventData(uniqueId, eventDataId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
