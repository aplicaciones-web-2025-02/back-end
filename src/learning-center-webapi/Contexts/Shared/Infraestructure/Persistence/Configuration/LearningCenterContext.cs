using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;

public class LearningCenterContext(DbContextOptions options) : DbContext(options)
{
    private DbSet<Tutorial> Tutorials { get; set; }
    private DbSet<Chapter> Chapters { get; set; }
    private DbSet<Enrolment> Enrolments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tutorial>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.CreatedDate).IsRequired();
            entity.Property(t => t.UpdatedDate).IsRequired(false);
            entity.Property(t => t.Title).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Description).HasMaxLength(200).HasColumnName("TutorialDescription");
            entity.Property(t => t.Author).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Level).IsRequired().HasDefaultValue(1);
            entity.Property(t => t.IsPublished).IsRequired().HasDefaultValue(false);
            entity.Property(t => t.Views).IsRequired().HasDefaultValue(0);
            entity.Property(t => t.Tags).HasMaxLength(200);
            entity.HasIndex(t => t.Title).IsUnique();
            entity.HasMany(t => t.Chapters)
                .WithOne(c => c.Tutorial)
                .HasForeignKey(c => c.TutorialId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Chapter>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.CreatedDate).IsRequired();
            entity.Property(c => c.UpdatedDate).IsRequired(false);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Pages).IsRequired().HasColumnName("TotalPages").HasDefaultValue(1);
            entity.Property(c => c.Order).IsRequired().HasDefaultValue(1);
            entity.Property(c => c.Summary).HasMaxLength(300);
            entity.Property(c => c.Duration).IsRequired(false);
            entity.Property(c => c.TutorialId).IsRequired();
            entity.HasIndex(c => c.Name);
            entity.HasIndex(c => new { c.TutorialId, c.Order }).IsUnique();
        });

        builder.Entity<Enrolment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedDate).IsRequired();
            entity.Property(e => e.UpdatedDate).IsRequired(false);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.TutorialId).IsRequired();
            entity.Property(e => e.EnrolmentDate).IsRequired();
            entity.Property(e => e.Progress).IsRequired().HasDefaultValue(0.0);
            entity.Property(e => e.CompletionDate).IsRequired(false);
            entity.HasIndex(e => new { e.UserId, e.TutorialId }).IsUnique();
            entity.HasOne(e => e.Tutorial)
                .WithMany()
                .HasForeignKey(e => e.TutorialId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}