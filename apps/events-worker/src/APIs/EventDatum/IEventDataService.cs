using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;

namespace EventsWorker.APIs;

public interface IEventDataService
{
    /// <summary>
    /// Create one EventData
    /// </summary>
    public Task<EventDatum> CreateEventDatum(EventDatumCreateInput eventdatum);

    /// <summary>
    /// Delete one EventData
    /// </summary>
    public Task DeleteEventDatum(EventDatumWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many EventDataItems
    /// </summary>
    public Task<List<EventDatum>> EventData(EventDatumFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about EventData records
    /// </summary>
    public Task<MetadataDto> EventDataMeta(EventDatumFindManyArgs findManyArgs);

    /// <summary>
    /// Get one EventData
    /// </summary>
    public Task<EventDatum> EventDatum(EventDatumWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one EventData
    /// </summary>
    public Task UpdateEventDatum(
        EventDatumWhereUniqueInput uniqueId,
        EventDatumUpdateInput updateDto
    );

    /// <summary>
    /// Get a Group record for EventData
    /// </summary>
    public Task<Group> GetGroup(EventDatumWhereUniqueInput uniqueId);
}
