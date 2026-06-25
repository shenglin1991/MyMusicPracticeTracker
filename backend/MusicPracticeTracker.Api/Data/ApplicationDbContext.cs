using Microsoft.EntityFrameworkCore;
using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<PracticeSession> PracticeSessions => Set<PracticeSession>();
    public DbSet<PracticeGoal> PracticeGoals => Set<PracticeGoal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.ToTable("Instruments");
            entity.HasKey(instrument => instrument.Id);
            entity.Property(instrument => instrument.Name).HasMaxLength(120).IsRequired();
            entity.Property(instrument => instrument.Color).HasMaxLength(32).IsRequired();
            entity.Property(instrument => instrument.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<PracticeSession>(entity =>
        {
            entity.ToTable("PracticeSessions");
            entity.HasKey(session => session.Id);
            entity.Property(session => session.StartTime).IsRequired();
            entity.Property(session => session.EndTime).IsRequired();
            entity.Property(session => session.DurationMinutes).IsRequired();
            entity.Property(session => session.Notes).HasMaxLength(1000);
            entity.Property(session => session.CreatedAt).IsRequired();
            entity.HasOne(session => session.Instrument)
                .WithMany(instrument => instrument.PracticeSessions)
                .HasForeignKey(session => session.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PracticeGoal>(entity =>
        {
            entity.ToTable("PracticeGoals");
            entity.HasKey(goal => goal.Id);
            entity.Property(goal => goal.DailyTargetMinutes).IsRequired();
            entity.Property(goal => goal.WeeklyTargetMinutes).IsRequired();
            entity.Property(goal => goal.CreatedAt).IsRequired();

            entity.HasData(new PracticeGoal
            {
                Id = 1,
                DailyTargetMinutes = 30,
                WeeklyTargetMinutes = 180,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        });
    }
}
