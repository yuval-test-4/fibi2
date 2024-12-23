using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using EventsWorker.APIs.Extensions;
using EventsWorker.Infrastructure;
using EventsWorker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWorker.APIs;

public abstract class EventDataItemsServiceBase : IEventDataItemsService
{
    protected readonly EventsWorkerDbContext _context;

    public EventDataItemsServiceBase(EventsWorkerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one EventData
    /// </summary>
    public async Task<EventData> CreateEventData(EventDataCreateInput createDto)
    {
        var eventData = new EventDataDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Message = createDto.Message,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            eventData.Id = createDto.Id;
        }
        if (createDto.Group != null)
        {
            eventData.Group = await _context
                .Groups.Where(group => createDto.Group.Id == group.Id)
                .FirstOrDefaultAsync();
        }

        _context.EventDataItems.Add(eventData);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EventDataDbModel>(eventData.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one EventData
    /// </summary>
    public async Task DeleteEventData(EventDataWhereUniqueInput uniqueId)
    {
        var eventData = await _context.EventDataItems.FindAsync(uniqueId.Id);
        if (eventData == null)
        {
            throw new NotFoundException();
        }

        _context.EventDataItems.Remove(eventData);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many EventDataItems
    /// </summary>
    public async Task<List<EventData>> EventDataItems(EventDataFindManyArgs findManyArgs)
    {
        var eventDataItems = await _context
            .EventDataItems.Include(x => x.Group)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return eventDataItems.ConvertAll(eventData => eventData.ToDto());
    }

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    public async Task<MetadataDto> EventDataItemsMeta(EventDataFindManyArgs findManyArgs)
    {
        var count = await _context.EventDataItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one EventData
    /// </summary>
    public async Task<EventData> EventData(EventDataWhereUniqueInput uniqueId)
    {
        var eventDataItems = await this.EventDataItems(
            new EventDataFindManyArgs { Where = new EventDataWhereInput { Id = uniqueId.Id } }
        );
        var eventData = eventDataItems.FirstOrDefault();
        if (eventData == null)
        {
            throw new NotFoundException();
        }

        return eventData;
    }

    /// <summary>
    /// Update one EventData
    /// </summary>
    public async Task UpdateEventData(
        EventDataWhereUniqueInput uniqueId,
        EventDataUpdateInput updateDto
    )
    {
        var eventData = updateDto.ToModel(uniqueId);

        if (updateDto.Group != null)
        {
            eventData.Group = await _context
                .Groups.Where(group => updateDto.Group == group.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(eventData).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.EventDataItems.Any(e => e.Id == eventData.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Group record for EventData
    /// </summary>
    public async Task<Group> GetGroup(EventDataWhereUniqueInput uniqueId)
    {
        var eventData = await _context
            .EventDataItems.Where(eventData => eventData.Id == uniqueId.Id)
            .Include(eventData => eventData.Group)
            .FirstOrDefaultAsync();
        if (eventData == null)
        {
            throw new NotFoundException();
        }
        return eventData.Group.ToDto();
    }
}
