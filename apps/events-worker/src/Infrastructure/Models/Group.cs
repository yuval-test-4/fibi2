using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsWorker.Infrastructure.Models;

[Table("Groups")]
public class GroupDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EventDatumDbModel>? EventData { get; set; } = new List<EventDatumDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
