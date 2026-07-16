using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; } = null!;
    public DbSet<Subtask> Subtasks { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<TimerSession> TimerSessions { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TaskItem
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired();
            entity.Property(e => e.IsAtivo).HasDefaultValue(true);
            entity.Property(e => e.TempoTotalGastoSegundos).HasDefaultValue(0);

            entity.HasOne(e => e.Tag)
                  .WithMany(t => t.TaskItems)
                  .HasForeignKey(e => e.TagId)
                  .IsRequired(false);
        });

        // Subtask
        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired();
            entity.Property(e => e.IsAtivo).HasDefaultValue(true);
            entity.Property(e => e.TempoTotalGastoSegundos).HasDefaultValue(0);

            entity.HasOne(e => e.ParentTask)
                  .WithMany(t => t.Subtasks)
                  .HasForeignKey(e => e.ParentTaskId)
                  .IsRequired(true)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Tag
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CorHexadecimal).IsRequired();
        });

        // TimerSession
        modelBuilder.Entity<TimerSession>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Tipo)
                  .HasConversion<string>();
        });
    }
}
