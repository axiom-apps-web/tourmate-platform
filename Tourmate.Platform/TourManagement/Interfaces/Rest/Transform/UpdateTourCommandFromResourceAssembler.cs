using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

public static class UpdateTourCommandFromResourceAssembler
{
    public static UpdateTourCommand ToCommandFromResource(UpdateTourResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentException(nameof(resource), "UpdateTourResource cannot be null when converting to command.");
        }

        return new UpdateTourCommand(
            resource.Title,
            resource.Description,
            resource.Duration,
            Enum.Parse<EDifficultyLevel>(resource.DifficultyLevel, ignoreCase: true),
            resource.Amount,
            resource.Currency
        );

    }
}