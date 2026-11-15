using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.TourManagement.Interfaces.Rest.Resources;

namespace Tourmate.Platform.TourManagement.Interfaces.Rest.Transform;

public static class CreateTourCommandFromResourceAssembler
{
    public static CreateTourCommand ToCommandFromResource(CreateTourResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentException(nameof(resource), "CreateTourResource cannot be null when converting to command.");
        }

        return new CreateTourCommand(
            resource.AgencyId,
            resource.Title,
            resource.Description,
            resource.Duration,
            Enum.Parse<EDifficultyLevel>(resource.DifficultyLevel, ignoreCase: true),
            resource.Amount,
            resource.Currency
        );

    }
}