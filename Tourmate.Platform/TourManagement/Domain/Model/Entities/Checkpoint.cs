using Tourmate.Platform.TourManagement.Domain.Model.ValueObjects;

namespace Tourmate.Platform.TourManagement.Domain.Model.Entities;

public class Checkpoint
{
    public CheckpointId Id { get; }
    public string Name { get; private set; }
    public Coordinates Coordinates { get; private set; }
    public int Order { get; private set; }
    public string Description { get; private set; }

    private Checkpoint()
    {
        Name = string.Empty;
        Description =  string.Empty;
        Coordinates = new Coordinates();
    }

    public Checkpoint(string name, double xaxis, double yaxis, int order, string description)
    {
        Name = name;
        Coordinates = new Coordinates(xaxis, yaxis);
        Order = order;
        Description = description;
    }
}