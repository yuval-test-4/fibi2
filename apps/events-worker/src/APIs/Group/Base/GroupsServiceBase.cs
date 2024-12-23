using EventsWorker.APIs;
using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;
using EventsWorker.APIs.Errors;
using EventsWorker.APIs.Extensions;
using EventsWorker.Infrastructure;
using EventsWorker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWorker.APIs;

public abstract class GroupsServiceBase : IGroupsService
{
    protected readonly EventsWorkerDbContext _context;

    public GroupsServiceBase(EventsWorkerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Group
    /// </summary>
    public async Task<Group> CreateGroup(GroupCreateInput createDto)
    {
        var group = new GroupDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            group.Id = createDto.Id;
        }
        if (createDto.EventData != null)
        {
            group.EventData = await _context
                .EventData.Where(eventDatum =>
                    createDto.EventData.Select(t => t.Id).Contains(eventDatum.Id)
                )
                .ToListAsync();
        }

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GroupDbModel>(group.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Group
    /// </summary>
    public async Task DeleteGroup(GroupWhereUniqueInput uniqueId)
    {
        var group = await _context.Groups.FindAsync(uniqueId.Id);
        if (group == null)
        {
            throw new NotFoundException();
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Groups
    /// </summary>
    public async Task<List<Group>> Groups(GroupFindManyArgs findManyArgs)
    {
        var groups = await _context
            .Groups.Include(x => x.EventData)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return groups.ConvertAll(group => group.ToDto());
    }

    /// <summary>
    /// Meta data about Group records
    /// </summary>
    public async Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs)
    {
        var count = await _context.Groups.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Group
    /// </summary>
    public async Task<Group> Group(GroupWhereUniqueInput uniqueId)
    {
        var groups = await this.Groups(
            new GroupFindManyArgs { Where = new GroupWhereInput { Id = uniqueId.Id } }
        );
        var group = groups.FirstOrDefault();
        if (group == null)
        {
            throw new NotFoundException();
        }

        return group;
    }

    /// <summary>
    /// Update one Group
    /// </summary>
    public async Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto)
    {
        var group = updateDto.ToModel(uniqueId);

        if (updateDto.EventData != null)
        {
            group.EventData = await _context
                .EventData.Where(eventDatum =>
                    updateDto.EventData.Select(t => t).Contains(eventDatum.Id)
                )
                .ToListAsync();
        }

        _context.Entry(group).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Groups.Any(e => e.Id == group.Id))
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
    /// Connect multiple EventData records to Group
    /// </summary>
    public async Task ConnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Groups.Include(x => x.EventData)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EventData.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.EventData);

        foreach (var child in childrenToConnect)
        {
            parent.EventData.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple EventData records from Group
    /// </summary>
    public async Task DisconnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Groups.Include(x => x.EventData)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EventData.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.EventData?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple EventData records for Group
    /// </summary>
    public async Task<List<EventDatum>> FindEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumFindManyArgs groupFindManyArgs
    )
    {
        var eventData = await _context
            .EventData.Where(m => m.GroupId == uniqueId.Id)
            .ApplyWhere(groupFindManyArgs.Where)
            .ApplySkip(groupFindManyArgs.Skip)
            .ApplyTake(groupFindManyArgs.Take)
            .ApplyOrderBy(groupFindManyArgs.SortBy)
            .ToListAsync();

        return eventData.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple EventData records for Group
    /// </summary>
    public async Task UpdateEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] childrenIds
    )
    {
        var group = await _context
            .Groups.Include(t => t.EventData)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (group == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EventData.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        group.EventData = children;
        await _context.SaveChangesAsync();
    }
}
