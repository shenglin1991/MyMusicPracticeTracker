using System.ComponentModel.DataAnnotations;

namespace MusicPracticeTracker.Api.DTOs.PracticeSessions;

public class CreatePracticeSessionRequest
{
    [Required]
    public Guid InstrumentId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }
}
