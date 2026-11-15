using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.TourManagement.Domain.Model.Commands;
using Tourmate.Platform.TourManagement.Domain.Model.Entities;
using Tourmate.Platform.TourManagement.Domain.Model.Events;
using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Aggregates;

public partial class TourSchedule : AggregateRoot
{
    
    public TourScheduleId Id { get; }
    public TourId TourId { get; private set; }
    public DepartureDateTime DepartureDateTime { get; private set; }
    
    public int MaxCapacity { get; private set; }
    public List<Participant> Participants { get; private set; }
    public ETourScheduleStatus Status { get; private set; }

    private TourSchedule()
    {
        TourId = new TourId();
        DepartureDateTime = new DepartureDateTime();
        Participants = new List<Participant>();
        Status = ETourScheduleStatus.Opened;
    }

    public TourSchedule(TourId tourId, DateTimeOffset departureDateTime, int maxCapacity)
    {
        TourId = tourId;
        DepartureDateTime = new DepartureDateTime(departureDateTime);
        MaxCapacity = maxCapacity;
        Participants = new List<Participant>();
        Status = ETourScheduleStatus.Opened;
        AddDomainEvent(new TourScheduleCreatedEvent(Id,  DepartureDateTime, MaxCapacity));
    }

    public TourSchedule(CreateTourScheduleCommand command) : this(command.TourId, command.DepartureDateTime, command.MaxCapacity)
    {
    }
    
}