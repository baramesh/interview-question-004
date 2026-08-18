namespace Example.InterviewQuestion004.Api.Models;

public sealed class Occupation
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<CandidateProfile> CandidateProfiles { get; set; } = [];
}
