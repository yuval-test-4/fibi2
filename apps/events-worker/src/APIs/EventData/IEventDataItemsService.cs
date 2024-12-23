using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;

namespace EventsWorker.APIs;

public interface IEventDataItemsService
{
    /// <summary>
    /// Create one EventData
    /// </summary>
    public Task<EventData> CreateEventData(EventDataCreateInput eventdata);

    /// <summary>
    /// Delete one EventData
    /// </summary>
    public Task DeleteEventData(EventDataWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many EventDataItems
    /// </summary>
    public Task<List<EventData>> EventDataItems(EventDataFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    public Task<MetadataDto> EventDataItemsMeta(EventDataFindManyArgs findManyArgs);

    /// <summary>
    /// Get one EventData
    /// </summary>
    public Task<EventData> EventData(EventDataWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one EventData
    /// </summary>
    public Task UpdateEventData(EventDataWhereUniqueInput uniqueId, EventDataUpdateInput updateDto);

    /// <summary>
    /// Get a Group record for EventData
    /// </summary>
    public Task<Group> GetGroup(EventDataWhereUniqueInput uniqueId);
}
