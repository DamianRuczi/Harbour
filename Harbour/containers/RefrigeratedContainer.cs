namespace Harbour.containers
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }
        
        private static readonly Dictionary<string, double> RequiredTemperatures = new Dictionary<string, double>
        {
            { "Bananas", 13.3 },
            { "Chocolate", 18.0 },
            { "Fish", 2.0 },
            { "Meat", -15.0 },
            { "Ice cream", -18.0 },
            { "Frozen pizza", -30.0 },
            { "Cheese", 7.2 },
            { "Sausages", 5.0 },
            { "Butter", 20.5 },
            { "Eggs", 19.0 }
        };

        public RefrigeratedContainer(double maxCapacity, double ownWeight, int height, int depth, string productType, double temperature)
            : base(maxCapacity, ownWeight, height, depth, "C")
        {
            if (!RequiredTemperatures.ContainsKey(productType))
                throw new ArgumentException($"Unknown product type: {productType}. Supported types: {string.Join(", ", RequiredTemperatures.Keys)}");

            if (temperature < RequiredTemperatures[productType])
                throw new ArgumentException($"Temperature {temperature}°C is too low for {productType}. Required: {RequiredTemperatures[productType]}°C or higher.");

            ProductType = productType;
            Temperature = temperature;
        }

        public void LoadCargo(double mass, string productType)
        {
            if (productType != ProductType)
                throw new ArgumentException($"Cannot load {productType} into a container designed for {ProductType}.");

            base.LoadCargo(mass);
        }

        
        public override void LoadCargo(double mass)
        {
            throw new InvalidOperationException("For refrigerated containers, use LoadCargo(double mass, string productType) to specify the product type.");
        }

        public override string ToString()
        {
            return base.ToString() + $", Product Type: {ProductType}, Temperature: {Temperature}°C";
        }
    }
}