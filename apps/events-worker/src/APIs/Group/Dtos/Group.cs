namespace EventsWorker.APIs.Dtos;

public class Group
{
    public DateTime CreatedAt { get; set; }

    public List<string>? EventData { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
