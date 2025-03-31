namespace Harbour.containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(double maxCapacity, double ownWeight, int height, int depth, bool isHazardous)
        : base(maxCapacity, ownWeight, height, depth, "L")
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double mass)
    {
        double allowedCapacity = IsHazardous ? MaxCapacity * 0.5 : MaxCapacity * 0.9;
        double predictedCapacity = CargoMass + mass;
        if (predictedCapacity > allowedCapacity)
        {
            NotifyHazard($"Attempted to overfill {SerialNumber}. Max allowed: {allowedCapacity} kg.");
            throw new OverfillException($"Cannot load {predictedCapacity} kg. Effective capacity is {allowedCapacity} kg.");
        }
        base.LoadCargo(mass);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Notification: {message}");
    }
}