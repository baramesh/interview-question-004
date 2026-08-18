using Example.InterviewQuestion004.Api.Contracts;
using Example.InterviewQuestion004.Api.Data;
using Example.InterviewQuestion004.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.InterviewQuestion004.Api.Controllers;

[ApiController]
[Route("api/candidate-profiles")]
public sealed class CandidateProfilesController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CreateCandidateProfileResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateCandidateProfileResponse>> Create(
        CreateCandidateProfileRequest request,
        CancellationToken cancellationToken)
    {
        if (!CreateCandidateProfileRequest.TryParseBirthDate(request.BirthDate, out var birthDate))
        {
            return ValidationProblem();
        }

        var profile = new CandidateProfile
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Phone = request.Phone.Trim(),
            ProfileBase64 = request.ProfileBase64,
            BirthDate = birthDate,
            Occupation = request.Occupation,
            Sex = request.Sex,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.CandidateProfiles.Add(profile);
        await dbContext.SaveChangesAsync(cancellationToken);

        return StatusCode(StatusCodes.Status201Created,
            new CreateCandidateProfileResponse(profile.Id, "save data success"));
    }
}

public sealed record CreateCandidateProfileResponse(int Id, string Message);
