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
    /// Connect multiple EventDataItems records to Group
    /// </summary>
    [HttpPost("{Id}/eventDataItems")]
    public async Task<ActionResult> ConnectEventDataItems(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] EventDataWhereUniqueInput[] eventDataItemsId
    )
    {
        try
        {
            await _service.ConnectEventDataItems(uniqueId, eventDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple EventDataItems records from Group
    /// </summary>
    [HttpDelete("{Id}/eventDataItems")]
    public async Task<ActionResult> DisconnectEventDataItems(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromBody()] EventDataWhereUniqueInput[] eventDataItemsId
    )
    {
        try
        {
            await _service.DisconnectEventDataItems(uniqueId, eventDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple EventDataItems records for Group
    /// </summary>
    [HttpGet("{Id}/eventDataItems")]
    public async Task<ActionResult<List<EventData>>> FindEventDataItems(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] EventDataFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEventDataItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple EventDataItems records for Group
    /// </summary>
    [HttpPatch("{Id}/eventDataItems")]
    public async Task<ActionResult> UpdateEventDataItems(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromBody()] EventDataWhereUniqueInput[] eventDataItemsId
    )
    {
        try
        {
            await _service.UpdateEventDataItems(uniqueId, eventDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
