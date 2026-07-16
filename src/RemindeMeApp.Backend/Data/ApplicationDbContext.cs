using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems { get; set; } = null!;
    public DbSet<Subtask> Subtasks { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<TimerSession> TimerSessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Model Configurations (Task 2.1)
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired();
            
            entity.HasOne(e => e.Tag)
                  .WithMany(t => t.TaskItems)
                  .HasForeignKey(e => e.TagId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(e => e.Subtasks)
                  .WithOne(s => s.ParentTask)
                  .HasForeignKey(s => s.ParentTaskId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CorHexadecimal).IsRequired();
        });

        modelBuilder.Entity<TimerSession>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}
