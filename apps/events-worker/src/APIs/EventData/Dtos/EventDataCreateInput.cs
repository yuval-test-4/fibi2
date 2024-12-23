namespace EventsWorker.APIs.Dtos;

public class EventDataCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Group? Group { get; set; }

    public string? Id { get; set; }

    public string? Message { get; set; }

    public DateTime UpdatedAt { get; set; }
}
