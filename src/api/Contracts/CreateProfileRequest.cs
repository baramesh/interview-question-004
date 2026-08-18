using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Example.InterviewQuestion004.Api.Contracts;

public sealed partial class CreateProfileRequest : IValidatableObject
{
    public const int MaximumImageBytes = 2 * 1024 * 1024;

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

    [Required, MaxLength(50)]
    public string OccupationCode { get; init; } = string.Empty;

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
            error = "Profile must be a Base64 encoded PNG or JPEG image.";
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

            var declaredMime = value[5..value.IndexOf(';')].ToLowerInvariant();
            if (!HasMatchingImageSignature(declaredMime, bytes))
            {
                error = "Profile image content does not match its declared MIME type.";
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

    private static bool HasMatchingImageSignature(string mime, ReadOnlySpan<byte> bytes) => mime switch
    {
        "image/png" => bytes.StartsWith(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
        "image/jpeg" => bytes.StartsWith(new byte[] { 0xFF, 0xD8, 0xFF }),
        _ => false
    };

    [GeneratedRegex(@"^\+?[0-9](?:[0-9 .()-]{7,18}[0-9])$")]
    private static partial Regex PhoneRegex();

    [GeneratedRegex(@"^data:image/(?:png|jpeg);base64,$", RegexOptions.IgnoreCase)]
    private static partial Regex ImageDataUrlRegex();
}
