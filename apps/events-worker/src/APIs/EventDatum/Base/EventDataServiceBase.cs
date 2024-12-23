using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using EventsWorker.APIs.Extensions;
using EventsWorker.Infrastructure;
using EventsWorker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWorker.APIs;

public abstract class EventDataServiceBase : IEventDataService
{
    protected readonly EventsWorkerDbContext _context;

    public EventDataServiceBase(EventsWorkerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one EventData
    /// </summary>
    public async Task<EventDatum> CreateEventDatum(EventDatumCreateInput createDto)
    {
        var eventDatum = new EventDatumDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Message = createDto.Message,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            eventDatum.Id = createDto.Id;
        }
        if (createDto.Group != null)
        {
            eventDatum.Group = await _context
                .Groups.Where(group => createDto.Group.Id == group.Id)
                .FirstOrDefaultAsync();
        }

        _context.EventData.Add(eventDatum);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EventDatumDbModel>(eventDatum.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one EventData
    /// </summary>
    public async Task DeleteEventDatum(EventDatumWhereUniqueInput uniqueId)
    {
        var eventDatum = await _context.EventData.FindAsync(uniqueId.Id);
        if (eventDatum == null)
        {
            throw new NotFoundException();
        }

        _context.EventData.Remove(eventDatum);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many EventDataItems
    /// </summary>
    public async Task<List<EventDatum>> EventData(EventDatumFindManyArgs findManyArgs)
    {
        var eventData = await _context
            .EventData.Include(x => x.Group)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return eventData.ConvertAll(eventDatum => eventDatum.ToDto());
    }

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    public async Task<MetadataDto> EventDataMeta(EventDatumFindManyArgs findManyArgs)
    {
        var count = await _context.EventData.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one EventData
    /// </summary>
    public async Task<EventDatum> EventDatum(EventDatumWhereUniqueInput uniqueId)
    {
        var eventData = await this.EventData(
            new EventDatumFindManyArgs { Where = new EventDatumWhereInput { Id = uniqueId.Id } }
        );
        var eventDatum = eventData.FirstOrDefault();
        if (eventDatum == null)
        {
            throw new NotFoundException();
        }

        return eventDatum;
    }

    /// <summary>
    /// Update one EventData
    /// </summary>
    public async Task UpdateEventDatum(
        EventDatumWhereUniqueInput uniqueId,
        EventDatumUpdateInput updateDto
    )
    {
        var eventDatum = updateDto.ToModel(uniqueId);

        if (updateDto.Group != null)
        {
            eventDatum.Group = await _context
                .Groups.Where(group => updateDto.Group == group.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(eventDatum).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.EventData.Any(e => e.Id == eventDatum.Id))
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
    public async Task<Group> GetGroup(EventDatumWhereUniqueInput uniqueId)
    {
        var eventDatum = await _context
            .EventData.Where(eventDatum => eventDatum.Id == uniqueId.Id)
            .Include(eventDatum => eventDatum.Group)
            .FirstOrDefaultAsync();
        if (eventDatum == null)
        {
            throw new NotFoundException();
        }
        return eventDatum.Group.ToDto();
    }
}
