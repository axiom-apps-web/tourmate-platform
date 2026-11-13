using Tourmate.Platform.Shared.Domain.Model.Aggregates.Internal;
using Tourmate.Platform.TourMonitoring.Domain.Model.Commands;
using Tourmate.Platform.TourMonitoring.Domain.Model.Events;
using Tourmate.Platform.TourMonitoring.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourMonitoring.Domain.Model.Aggregates;

public partial class ActiveTour: AggregateRoot
{
    public ActiveTour()
    {
        Status = EStatus.IN_PROGRESS;
        CurrentLatitude = 0;
        CurrentLongitude = 0;
       

    }
    
    public ActiveTour( TourScheduleId tourScheduleId, GuideId guideId, double currentLatitude, double currentLongitude) : this()
    {
        
        TourScheduleId = tourScheduleId;
        GuideId = guideId;
        CurrentLatitude = 0;
        CurrentLongitude = 0;
        StartedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new ActiveTourCreatedEvent(Id, TourScheduleId, GuideId));
    }
    
    public ActiveTour(int tourScheduleId, int guideId, double currentLatitude, double currentLongitude) : this(new TourScheduleId(tourScheduleId), new GuideId(guideId), currentLatitude, currentLongitude)
    {
    }
    
    public ActiveTour(CreateActiveTourCommand command) : this(command.TourScheduleId, command.GuideId, command.CurrentLatitude, command.CurrentLongitude)
    {
    }
    
    public ActiveTourId Id { get;  }
    public TourScheduleId TourScheduleId { get; private set; }
    public GuideId GuideId { get; private set;  }
    public EStatus Status { get; private set; }
    public double CurrentLatitude { get; private set; }
    public double CurrentLongitude { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public DateTimeOffset? FinishedAt { get; private set; }
    
}