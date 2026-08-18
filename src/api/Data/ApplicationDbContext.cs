using Example.InterviewQuestion004.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var profile = modelBuilder.Entity<CandidateProfile>();
        profile.ToTable("candidate_profiles");
        profile.Property(item => item.Id).HasColumnName("id");
        profile.Property(item => item.FirstName).HasColumnName("first_name");
        profile.Property(item => item.LastName).HasColumnName("last_name");
        profile.Property(item => item.Email).HasColumnName("email");
        profile.Property(item => item.Phone).HasColumnName("phone");
        profile.Property(item => item.ProfileBase64).HasColumnName("profile_base64");
        profile.Property(item => item.BirthDate).HasColumnName("birth_date");
        profile.Property(item => item.Occupation).HasColumnName("occupation");
        profile.Property(item => item.Sex).HasColumnName("sex");
        profile.Property(item => item.CreatedAtUtc).HasColumnName("created_at_utc");
    }
}
