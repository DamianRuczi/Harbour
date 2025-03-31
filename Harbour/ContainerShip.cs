namespace Harbour;

public class ContainerShip
    {
        public string Name { get; private set; }
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; }
        private List<Container> Containers { get; set; }

        public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
                throw new Exception("Max container count reached.");
            if ((GetTotalWeight() + (container.CargoMass + container.OwnWeight) / 1000) > MaxWeight)
                throw new Exception("Max weight exceeded.");
            Containers.Add(container);
        }

        public void LoadContainers(List<Container> containers)
        {
            foreach (var container in containers)
                LoadContainer(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null) Containers.Remove(container);
        }

        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            UnloadContainer(serialNumber);
            LoadContainer(newContainer);
        }

        public void TransferContainer(ContainerShip otherShip, string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                UnloadContainer(serialNumber);
                otherShip.LoadContainer(container);
            }
        }

        private double GetTotalWeight()
        {
            return Containers.Sum(c => (c.CargoMass + c.OwnWeight) / 1000);
        }

        public override string ToString()
        {
            return $"{Name} (Speed: {MaxSpeed} knots, Max Containers: {MaxContainerCount}, Max Weight: {MaxWeight} tons, Current Containers: {Containers.Count})";
        }

        public void PrintCargo()
        {
            Console.WriteLine($"Containers on {Name}:");
            foreach (var container in Containers)
                Console.WriteLine(container);
        }
    }