using EventsWorker.APIs.Dtos;
using EventsWorker.Infrastructure.Models;

namespace EventsWorker.APIs.Extensions;

public static class EventDataItemsExtensions
{
    public static EventData ToDto(this EventDataDbModel model)
    {
        return new EventData
        {
            CreatedAt = model.CreatedAt,
            Group = model.GroupId,
            Id = model.Id,
            Message = model.Message,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EventDataDbModel ToModel(
        this EventDataUpdateInput updateDto,
        EventDataWhereUniqueInput uniqueId
    )
    {
        var eventData = new EventDataDbModel { Id = uniqueId.Id, Message = updateDto.Message };

        if (updateDto.CreatedAt != null)
        {
            eventData.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Group != null)
        {
            eventData.GroupId = updateDto.Group;
        }
        if (updateDto.UpdatedAt != null)
        {
            eventData.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return eventData;
    }
}
