using Example.InterviewQuestion004.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Controllers;

[ApiController]
[Route("api/occupations")]
public sealed class OccupationsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<OccupationOptionResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OccupationOptionResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var occupations = await dbContext.Occupations
            .AsNoTracking()
            .Where(item => item.IsActive)
            .OrderBy(item => item.DisplayOrder)
            .ThenBy(item => item.Name)
            .Select(item => new OccupationOptionResponse(item.Code, item.Name))
            .ToListAsync(cancellationToken);

        return Ok(occupations);
    }
}

public sealed record OccupationOptionResponse(string Code, string Name);
