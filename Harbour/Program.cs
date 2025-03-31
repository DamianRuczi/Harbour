using Harbour;
using Harbour.containers;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Tworzenie kontenerów dla każdego typu");
        var liquidContainer = new LiquidContainer(2000, 1000, 250, 300, true);
        Console.WriteLine(liquidContainer);
        var gasContainer = new GasContainer(1500, 800, 200, 250, 5.0);
        Console.WriteLine(gasContainer);
        var refrigeratedContainer = new RefrigeratedContainer(1000, 600, 220, 280, "Bananas", 14.0);
        Console.WriteLine(refrigeratedContainer);

        Console.WriteLine("Załadunek kontenerów");
        try
        {
            liquidContainer.LoadCargo(900);
            gasContainer.LoadCargo(1200);
            refrigeratedContainer.LoadCargo(800, "Bananas");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd załadunku: {ex.Message}");
        }
        Console.WriteLine();


        Console.WriteLine("Tworzenie statków");
        var ship1 = new ContainerShip("Pierwszy", 15.0, 5, 20.0); 
        var ship2 = new ContainerShip("Drugi", 12.0, 3, 10.0);
        Console.WriteLine(ship1);
        Console.WriteLine(ship2);
        Console.WriteLine();

        Console.WriteLine("Załadunek statku");
        try
        {
            ship1.LoadContainer(liquidContainer);
            ship1.LoadContainers(new List<Container> { gasContainer, refrigeratedContainer });
            ship1.PrintCargo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd załadunku na statek: {ex.Message}");
        }
        Console.WriteLine();

        Console.WriteLine("Usuniecie kontenera z gazem ze statku");
        ship1.UnloadContainer(gasContainer.SerialNumber);
        ship1.PrintCargo();
        Console.WriteLine();

        Console.WriteLine("Zamiana kontenera");
        var newLiquidContainer = new LiquidContainer(1800, 900, 240, 290, false);
        newLiquidContainer.LoadCargo(1600);
        ship1.ReplaceContainer(liquidContainer.SerialNumber, newLiquidContainer);
        ship1.PrintCargo();
        Console.WriteLine();


        Console.WriteLine("Transfer kontenerów miedzy statkami");
        ship1.TransferContainer(ship2, refrigeratedContainer.SerialNumber);
        ship1.PrintCargo();
        ship2.PrintCargo();
        Console.WriteLine();

        Console.WriteLine("Rozładowanie kontenera: ");
        Console.WriteLine(liquidContainer);
        liquidContainer.UnloadCargo();
        Console.WriteLine(liquidContainer);
        Console.WriteLine();

       
        try
        {
            gasContainer.LoadCargo(1500); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd overfill: {ex.Message}");
        }
        
        
        try
        {
            refrigeratedContainer.LoadCargo(1200, "Fish");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd typu załadunku : {ex.Message}");
        }
        
        
        try
        {
            ship2.LoadContainers(new List<Container> { liquidContainer, gasContainer, refrigeratedContainer });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd overfill dla statku: {ex.Message}");
        }
    }
}