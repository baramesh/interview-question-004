using Example.InterviewQuestion004.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Occupation> Occupations => Set<Occupation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var profile = modelBuilder.Entity<Profile>();
        profile.ToTable("profiles");
        profile.Property(item => item.Id).HasColumnName("id");
        profile.Property(item => item.FirstName).HasColumnName("first_name");
        profile.Property(item => item.LastName).HasColumnName("last_name");
        profile.Property(item => item.Email).HasColumnName("email");
        profile.Property(item => item.Phone).HasColumnName("phone");
        profile.Property(item => item.ProfileBase64).HasColumnName("profile_base64");
        profile.Property(item => item.BirthDate).HasColumnName("birth_date");
        profile.Property(item => item.OccupationId).HasColumnName("occupation_id");
        profile.Property(item => item.Sex).HasColumnName("sex");
        profile.Property(item => item.CreatedAtUtc).HasColumnName("created_at_utc");
        profile.HasOne(item => item.Occupation)
            .WithMany(item => item.Profiles)
            .HasForeignKey(item => item.OccupationId)
            .OnDelete(DeleteBehavior.Restrict);

        var occupation = modelBuilder.Entity<Occupation>();
        occupation.ToTable("occupations");
        occupation.HasKey(item => item.Id);
        occupation.Property(item => item.Id).HasColumnName("id");
        occupation.Property(item => item.Code).HasColumnName("code").HasMaxLength(50);
        occupation.Property(item => item.Name).HasColumnName("name").HasMaxLength(100);
        occupation.Property(item => item.DisplayOrder).HasColumnName("display_order");
        occupation.Property(item => item.IsActive).HasColumnName("is_active");
        occupation.HasIndex(item => item.Code).IsUnique();
        occupation.HasData(
            new Occupation { Id = 1, Code = "software-engineer", Name = "Software Engineer", DisplayOrder = 10 },
            new Occupation { Id = 2, Code = "business-analyst", Name = "Business Analyst", DisplayOrder = 20 },
            new Occupation { Id = 3, Code = "quality-assurance", Name = "Quality Assurance", DisplayOrder = 30 },
            new Occupation { Id = 4, Code = "ux-ui-designer", Name = "UX/UI Designer", DisplayOrder = 40 },
            new Occupation { Id = 5, Code = "project-manager", Name = "Project Manager", DisplayOrder = 50 });
    }
}
