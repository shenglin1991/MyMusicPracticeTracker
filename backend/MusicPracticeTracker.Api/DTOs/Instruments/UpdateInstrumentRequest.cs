using System.ComponentModel.DataAnnotations;

namespace MusicPracticeTracker.Api.DTOs.Instruments;

public class UpdateInstrumentRequest
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public string Color { get; set; } = "#2563eb";
}
