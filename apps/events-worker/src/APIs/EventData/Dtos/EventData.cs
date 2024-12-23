namespace EventsWorker.APIs.Dtos;

public class EventData
{
    public DateTime CreatedAt { get; set; }

    public string? Group { get; set; }

    public string Id { get; set; }

    public string? Message { get; set; }

    public DateTime UpdatedAt { get; set; }
}