namespace Example.InterviewQuestion004.Api.Models;

public sealed class CandidateProfile
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string ProfileBase64 { get; set; }
    public DateOnly BirthDate { get; set; }
    public int OccupationId { get; set; }
    public Occupation? Occupation { get; set; }
    public required string Sex { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
