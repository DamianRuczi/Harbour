namespace Harbour.containers;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double maxCapacity, double ownWeight, int height, int depth, double pressure)
        : base(maxCapacity, ownWeight, height, depth, "G")
    {
        Pressure = pressure;
    }

    public override void LoadCargo(double mass)
    {
        double predictedCapacity = CargoMass + mass;
        if (predictedCapacity > MaxCapacity)
        {
            NotifyHazard($"Overfill attempt on {SerialNumber}. Max capacity: {MaxCapacity} kg.");
            throw new OverfillException($"Cargo mass {predictedCapacity} kg exceeds max capacity {MaxCapacity} kg.");
        }
        base.LoadCargo(mass);
    }

    public override void UnloadCargo()
    {
        CargoMass = MaxCapacity * 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Notification: {message}");
    }
}