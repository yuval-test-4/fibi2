using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsWorker.Infrastructure.Models;

[Table("EventData")]
public class EventDatumDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    public GroupDbModel? Group { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Message { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
