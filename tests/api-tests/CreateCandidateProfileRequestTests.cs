using System.ComponentModel.DataAnnotations;
using Example.InterviewQuestion004.Api.Contracts;

namespace Example.InterviewQuestion004.Api.Tests;

public sealed class CreateCandidateProfileRequestTests
{
    [Fact]
    public void Valid_request_passes_validation()
    {
        var results = Validate(CreateValidRequest());
        Assert.Empty(results);
    }

    [Theory]
    [InlineData("31/02/2000")]
    [InlineData("2000-01-31")]
    [InlineData("01/01/2999")]
    public void Invalid_birth_date_fails_validation(string birthDate)
    {
        var request = CreateValidRequest(birthDate: birthDate);
        var results = Validate(request);
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(request.BirthDate)));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("12345")]
    public void Invalid_phone_fails_validation(string phone)
    {
        var request = CreateValidRequest(phone: phone);
        var results = Validate(request);
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(request.Phone)));
    }

    [Fact]
    public void Invalid_profile_data_fails_validation()
    {
        var request = CreateValidRequest(profileBase64: "not-an-image");
        var results = Validate(request);
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(request.ProfileBase64)));
    }

    [Fact]
    public void Mismatched_image_signature_fails_validation()
    {
        var jpegBytes = Convert.ToBase64String([0xFF, 0xD8, 0xFF, 0x00]);
        var request = CreateValidRequest(profileBase64: $"data:image/png;base64,{jpegBytes}");

        var results = Validate(request);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(request.ProfileBase64)));
    }

    [Theory]
    [InlineData("image/png", "iVBORw0KGgo=")]
    [InlineData("image/jpeg", "/9j/AA==")]
    [InlineData("image/gif", "R0lGODlh")]
    [InlineData("image/webp", "UklGRgAAAABXRUJQ")]
    public void Supported_image_signatures_pass_validation(string mime, string base64)
    {
        var request = CreateValidRequest(profileBase64: $"data:{mime};base64,{base64}");

        var results = Validate(request);

        Assert.DoesNotContain(results,
            result => result.MemberNames.Contains(nameof(request.ProfileBase64)));
    }

    [Fact]
    public void Missing_occupation_code_fails_validation()
    {
        var request = CreateValidRequest(occupationCode: string.Empty);
        var results = Validate(request);
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(request.OccupationCode)));
    }

    private static CreateCandidateProfileRequest CreateValidRequest(
        string birthDate = "18/08/1990",
        string phone = "+66 81 234 5678",
        string profileBase64 = "data:image/png;base64,iVBORw0KGgo=",
        string occupationCode = "software-engineer") => new()
        {
            FirstName = "Ada",
            LastName = "Lovelace",
            Email = "ada@example.com",
            Phone = phone,
            ProfileBase64 = profileBase64,
            BirthDate = birthDate,
            OccupationCode = occupationCode,
            Sex = "Female"
        };

    private static List<ValidationResult> Validate(CreateCandidateProfileRequest request)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);
        return results;
    }
}
