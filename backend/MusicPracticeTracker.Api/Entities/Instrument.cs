namespace MusicPracticeTracker.Api.Entities;

public class Instrument
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#2563eb";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PracticeSession> PracticeSessions { get; set; } = new List<PracticeSession>();
}
