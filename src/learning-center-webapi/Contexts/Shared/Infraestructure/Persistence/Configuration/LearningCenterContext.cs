using learning_center_webapi.Contexts.Shared.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;

public class LearningCenterContext(DbContextOptions options) : DbContext(options)
{
    DbSet<Tutorial> Tutorials { get; set; }
    DbSet<Chapter> Chapters { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tutorial>(entity =>
        {
            entity.Property(t => t.Title).IsRequired();
            entity.Property(t => t.Title).HasMaxLength(50);
            entity.Property(t => t.Title).HasDefaultValue("My title");            
            entity.Property(t => t.Title)

        });
    }
}