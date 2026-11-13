namespace Tourmate.Platform.FeedbackAndTourReviews.Domain.Model.ValueObjects;

/// <summary>
///     Rating value object. Represents the stars (from 1 to 5) a traveler gives to a tour.
/// </summary>
public record Rating
{
    public const int MinValue = 1;
    public const int MaxValue = 5;

    public Rating(int value)
    {
        if (!IsValid(value))
            throw new ArgumentOutOfRangeException(nameof(value), $"Rating must be between {MinValue} and {MaxValue}.");
        Value = value;
    }

    public int Value { get; }

    public static bool IsValid(int value) => value is >= MinValue and <= MaxValue;
}