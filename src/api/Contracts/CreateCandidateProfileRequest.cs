using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Example.InterviewQuestion004.Api.Contracts;

public sealed partial class CreateCandidateProfileRequest : IValidatableObject
{
    public const int MaximumImageBytes = 2 * 1024 * 1024;

    public static readonly IReadOnlySet<string> AllowedOccupations = new HashSet<string>(
        ["Software Engineer", "Business Analyst", "Quality Assurance", "UX/UI Designer", "Project Manager"],
        StringComparer.Ordinal);

    [Required, MaxLength(100)]
    public string FirstName { get; init; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; init; } = string.Empty;

    [Required, EmailAddress, MaxLength(254)]
    public string Email { get; init; } = string.Empty;

    [Required, MaxLength(30)]
    public string Phone { get; init; } = string.Empty;

    [Required]
    public string ProfileBase64 { get; init; } = string.Empty;

    [Required]
    public string BirthDate { get; init; } = string.Empty;

    [Required]
    public string Occupation { get; init; } = string.Empty;

    [Required]
    public string Sex { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!PhoneRegex().IsMatch(Phone))
        {
            yield return new ValidationResult("Please provide a valid phone number.", [nameof(Phone)]);
        }

        if (!DateOnly.TryParseExact(BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var birthDate) || birthDate >= DateOnly.FromDateTime(DateTime.UtcNow))
        {
            yield return new ValidationResult("Birth date must be a past date in DD/MM/YYYY format.",
                [nameof(BirthDate)]);
        }

        if (!AllowedOccupations.Contains(Occupation))
        {
            yield return new ValidationResult("Please select a valid occupation.", [nameof(Occupation)]);
        }

        if (Sex is not ("Male" or "Female"))
        {
            yield return new ValidationResult("Please select a valid sex.", [nameof(Sex)]);
        }

        if (!TryValidateImage(ProfileBase64, out var imageError))
        {
            yield return new ValidationResult(imageError, [nameof(ProfileBase64)]);
        }
    }

    public static bool TryParseBirthDate(string value, out DateOnly birthDate) =>
        DateOnly.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out birthDate);

    private static bool TryValidateImage(string value, out string error)
    {
        error = string.Empty;
        var separatorIndex = value.IndexOf(',');
        if (separatorIndex < 0 || !ImageDataUrlRegex().IsMatch(value[..(separatorIndex + 1)]))
        {
            error = "Profile must be a Base64 encoded PNG, JPEG, GIF or WebP image.";
            return false;
        }

        try
        {
            var bytes = Convert.FromBase64String(value[(separatorIndex + 1)..]);
            if (bytes.Length is 0 or > MaximumImageBytes)
            {
                error = "Profile image must be no larger than 2 MB.";
                return false;
            }
        }
        catch (FormatException)
        {
            error = "Profile contains invalid Base64 data.";
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"^\+?[0-9](?:[0-9 .()-]{7,18}[0-9])$")]
    private static partial Regex PhoneRegex();

    [GeneratedRegex(@"^data:image/(?:png|jpeg|gif|webp);base64,$", RegexOptions.IgnoreCase)]
    private static partial Regex ImageDataUrlRegex();
}
