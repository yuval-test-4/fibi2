using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;

namespace EventsWorker.APIs;

public interface IGroupsService
{
    /// <summary>
    /// Create one Group
    /// </summary>
    public Task<Group> CreateGroup(GroupCreateInput group);

    /// <summary>
    /// Delete one Group
    /// </summary>
    public Task DeleteGroup(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Groups
    /// </summary>
    public Task<List<Group>> Groups(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Group records
    /// </summary>
    public Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Group
    /// </summary>
    public Task<Group> Group(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Group
    /// </summary>
    public Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto);

    /// <summary>
    /// Connect multiple EventDataItems records to Group
    /// </summary>
    public Task ConnectEventDataItems(
        GroupWhereUniqueInput uniqueId,
        EventDataWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Disconnect multiple EventDataItems records from Group
    /// </summary>
    public Task DisconnectEventDataItems(
        GroupWhereUniqueInput uniqueId,
        EventDataWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Find multiple EventDataItems records for Group
    /// </summary>
    public Task<List<EventData>> FindEventDataItems(
        GroupWhereUniqueInput uniqueId,
        EventDataFindManyArgs EventDataFindManyArgs
    );

    /// <summary>
    /// Update multiple EventDataItems records for Group
    /// </summary>
    public Task UpdateEventDataItems(
        GroupWhereUniqueInput uniqueId,
        EventDataWhereUniqueInput[] eventDataId
    );
}
