namespace Harbour;

    public abstract class Container
    {
        public string SerialNumber { get; private set; }
        public double CargoMass { get; protected set; }
        public double MaxCapacity { get; private set; }
        public double OwnWeight { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }

        protected Container(double maxCapacity, double ownWeight, int height, int depth, string typeIdentifier)
        {
            SerialNumber = GenerateSerialNumber(typeIdentifier);
            MaxCapacity = maxCapacity;
            OwnWeight = ownWeight;
            Height = height;
            Depth = depth;
            CargoMass = 0;
        }

        private static int _serialCounter = 1;
        private string GenerateSerialNumber(string typeIdentifier)
        {
            return $"KON-{typeIdentifier}-{_serialCounter++}";
        }

        public virtual void LoadCargo(double mass)
        {
            if (mass > MaxCapacity)
                throw new OverfillException($"Cargo mass {mass} kg exceeds max capacity {MaxCapacity} kg.");
            CargoMass = mass;
        }

        public virtual void UnloadCargo()
        {
            CargoMass = 0;
        }

        public override string ToString()
        {
            return $"Serial: {SerialNumber}, Cargo Mass: {CargoMass} kg, Max Capacity: {MaxCapacity} kg";
        }
    }

    public class OverfillException(string message) : Exception(message);
