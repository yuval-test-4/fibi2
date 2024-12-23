using EventsWorker.APIs.Dtos;
using EventsWorker.Infrastructure.Models;

namespace EventsWorker.APIs.Extensions;

public static class EventDataExtensions
{
    public static EventDatum ToDto(this EventDatumDbModel model)
    {
        return new EventDatum
        {
            CreatedAt = model.CreatedAt,
            Group = model.GroupId,
            Id = model.Id,
            Message = model.Message,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EventDatumDbModel ToModel(
        this EventDatumUpdateInput updateDto,
        EventDatumWhereUniqueInput uniqueId
    )
    {
        var eventDatum = new EventDatumDbModel { Id = uniqueId.Id, Message = updateDto.Message };

        if (updateDto.CreatedAt != null)
        {
            eventDatum.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Group != null)
        {
            eventDatum.GroupId = updateDto.Group;
        }
        if (updateDto.UpdatedAt != null)
        {
            eventDatum.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return eventDatum;
    }
}
