using Example.InterviewQuestion004.Api.Contracts;
using Example.InterviewQuestion004.Api.Data;
using Example.InterviewQuestion004.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Controllers;

[ApiController]
[Route("api/profiles")]
public sealed class ProfilesController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPost]
    [EnableRateLimiting("profile-write")]
    [ProducesResponseType<CreateProfileResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProfileResponse>> Create(
        CreateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var occupationCode = request.OccupationCode.Trim().ToLowerInvariant();
        var occupation = await dbContext.Occupations
            .SingleOrDefaultAsync(item => item.Code == occupationCode && item.IsActive, cancellationToken);
        if (occupation is null)
        {
            ModelState.AddModelError(nameof(request.OccupationCode), "Please select a valid occupation.");
            return ValidationProblem(ModelState);
        }

        if (!CreateProfileRequest.TryParseBirthDate(request.BirthDate, out var birthDate))
        {
            return ValidationProblem();
        }

        var profile = new Profile
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Phone = request.Phone.Trim(),
            ProfileBase64 = request.ProfileBase64,
            BirthDate = birthDate,
            OccupationId = occupation.Id,
            Sex = request.Sex,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.Profiles.Add(profile);
        await dbContext.SaveChangesAsync(cancellationToken);

        return StatusCode(StatusCodes.Status201Created,
            new CreateProfileResponse(profile.Id, "save data success"));
    }
}

public sealed record CreateProfileResponse(int Id, string Message);
