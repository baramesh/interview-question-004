using Example.InterviewQuestion004.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();
}
