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
    /// Connect multiple EventData records to Group
    /// </summary>
    public Task ConnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Disconnect multiple EventData records from Group
    /// </summary>
    public Task DisconnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Find multiple EventData records for Group
    /// </summary>
    public Task<List<EventDatum>> FindEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumFindManyArgs EventDatumFindManyArgs
    );

    /// <summary>
    /// Update multiple EventData records for Group
    /// </summary>
    public Task UpdateEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );
}
