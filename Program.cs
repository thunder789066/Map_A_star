using Prg_1_map_A_star;

internal class Program
{
    private static void Main(string[] args)
    {
        string coordinatesFile = "Input\\coordinates.txt";
        string adjacenciesFile = "Input\\Adjacencies.txt";
        
        RouteFinder routeFinder = new RouteFinder(coordinatesFile, adjacenciesFile);
        
        List<City> cityList = routeFinder.getCityList();
        Console.WriteLine("Choose the Starting City & Destination City \r\n\tEx. 1,3\r\n");
        for (int i = 0; i < cityList.Count; i++)
        {
            City city = cityList[i];
            Console.WriteLine($"{i + 1}\t{city.CityName}");
        }
        string? str = Console.ReadLine();

        if (str == null)
        {
            Console.WriteLine("Error, try again later.");
            return;
        }
        string[] parts = str.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            Console.WriteLine("Error, try again later.");
            return;
        }

        string startingCity = cityList[int.Parse(parts[0])-1].CityName;
        string endingCity = cityList[int.Parse(parts[1])-1].CityName;

        Route route = routeFinder.FindRoute(startingCity, endingCity);

        //Display results, if any
        Console.WriteLine();
        Console.WriteLine("Route:");
        if (route != null)
        {
            foreach (var waypoint in route.Path)
            {
                Console.WriteLine("  " + waypoint.CityName);
            }
            // Console.WriteLine($"Total Distance: {route.GetRouteDistance(): 0.00}"); // Total actual cost
        }
        else
        {
            Console.WriteLine("  No route found.");
        }
    }
}