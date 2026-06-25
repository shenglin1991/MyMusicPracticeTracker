using System.ComponentModel.DataAnnotations;

namespace MusicPracticeTracker.Api.DTOs.Instruments;

public class CreateInstrumentRequest
{
    [Required]
    [MaxLength(120)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(32)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Color is required.")]
    public string Color { get; set; } = "#2563eb";
}
