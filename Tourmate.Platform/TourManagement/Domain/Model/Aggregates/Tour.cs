using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.Entities;
using Tourmate.Platform.TourManagement.Domain.Model.Events;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Aggregates;

public partial class Tour : AggregateRoot
{
    public TourId Id { get; }
    public AgencyId AgencyId { get; private set; }
    public TourDetails Details { get; private set; }
    
    public Money Price { get; private set; }
    public List<Checkpoint> Checkpoints { get; private set; }
    public ETourStatus Status { get; private set; }

    private Tour()
    {
        AgencyId = new AgencyId();
        Details = new TourDetails();
        Checkpoints = new List<Checkpoint>();
        Status = ETourStatus.Draft;
    }

    public Tour(AgencyId agencyId, string title, string description, string duration, EDifficultyLevel difficultyLevel,
        double amount, string currency)
    {
        AgencyId = agencyId;
        Details = new TourDetails(title, description, duration, difficultyLevel);
        Price = new Money(amount, currency);
        Checkpoints = new List<Checkpoint>();
        Status = ETourStatus.Draft;
        AddDomainEvent(new TourCreatedEvent(Id, AgencyId, Details.Title));
    }

    public Tour(CreateTourCommand command) : this(command.AgencyId, command.Title, command.Description,
        command.Duration, command.DifficultyLevel, command.Amount, command.Currency)
    {
    }

    public void ChangeDetails(string title, string description, string duration,
        EDifficultyLevel difficultyLevel, double amount, string currency)
    {
        Details = new TourDetails(title, description, duration, difficultyLevel);
        Price = new Money(amount, currency);
    }
}