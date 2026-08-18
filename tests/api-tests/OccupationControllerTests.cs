using Example.InterviewQuestion004.Api.Contracts;
using Example.InterviewQuestion004.Api.Controllers;
using Example.InterviewQuestion004.Api.Data;
using Example.InterviewQuestion004.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.InterviewQuestion004.Api.Tests;

public sealed class OccupationControllerTests
{
    [Fact]
    public async Task GetAll_returns_only_active_occupations_in_display_order()
    {
        await using var dbContext = CreateDbContext();
        dbContext.Occupations.AddRange(
            new Occupation { Id = 101, Code = "second", Name = "Second", DisplayOrder = 20 },
            new Occupation { Id = 102, Code = "first", Name = "First", DisplayOrder = 10 },
            new Occupation { Id = 103, Code = "inactive", Name = "Inactive", DisplayOrder = 1, IsActive = false });
        await dbContext.SaveChangesAsync();

        var controller = new OccupationsController(dbContext);
        var actionResult = await controller.GetAll(CancellationToken.None);
        var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsAssignableFrom<IReadOnlyList<OccupationOptionResponse>>(ok.Value);

        Assert.Collection(response,
            item => Assert.Equal("first", item.Code),
            item => Assert.Equal("second", item.Code));
    }

    [Fact]
    public async Task Create_rejects_unknown_occupation_code()
    {
        await using var dbContext = CreateDbContext();
        var controller = new ProfilesController(dbContext);

        var actionResult = await controller.Create(CreateValidRequest("unknown"), CancellationToken.None);
        var error = Assert.IsType<ObjectResult>(actionResult.Result);
        var problem = Assert.IsType<ValidationProblemDetails>(error.Value);

        Assert.Contains(nameof(CreateProfileRequest.OccupationCode), problem.Errors.Keys);
        Assert.Empty(dbContext.Profiles);
    }

    [Fact]
    public async Task Create_resolves_occupation_code_to_foreign_key()
    {
        await using var dbContext = CreateDbContext();
        dbContext.Occupations.Add(
            new Occupation { Id = 201, Code = "software-engineer", Name = "Software Engineer", DisplayOrder = 10 });
        await dbContext.SaveChangesAsync();
        var controller = new ProfilesController(dbContext);

        var actionResult = await controller.Create(
            CreateValidRequest("software-engineer"), CancellationToken.None);
        var created = Assert.IsType<ObjectResult>(actionResult.Result);
        var response = Assert.IsType<CreateProfileResponse>(created.Value);
        var profile = Assert.Single(dbContext.Profiles);

        Assert.Equal(201, created.StatusCode);
        Assert.True(response.Id > 0);
        Assert.Equal(201, profile.OccupationId);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    private static CreateProfileRequest CreateValidRequest(string occupationCode) => new()
    {
        FirstName = "Ada",
        LastName = "Lovelace",
        Email = "ada@example.com",
        Phone = "+66 81 234 5678",
        ProfileBase64 = "data:image/png;base64,iVBORw0KGgo=",
        BirthDate = "18/08/1990",
        OccupationCode = occupationCode,
        Sex = "Female"
    };
}
