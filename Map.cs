namespace Prg_1_map_A_star
{
    internal class Map
    {
        public List<City> Cities;

        public void LoadFiles(string coordinatesFile, string adjacenciesFile)
        {
            Cities = new List<City>();

            var coordinatesFileLines = File.ReadAllLines(coordinatesFile);
            var adjacenciesFileLines = File.ReadAllLines(adjacenciesFile);

            // Read list of coordinates
            foreach (string line in coordinatesFileLines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                City city = new City()
                {
                    CityName = parts[0],
                    Latitude = double.Parse(parts[1]),
                    Longitude = double.Parse(parts[2]),
                    AdjacentCities = new List<City>()
                };
                Cities.Add(city);
            }

            // Read and assemble adjacencies
            foreach (string line in adjacenciesFileLines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string cityOfInterestName = parts[0];

                //Find city of interest
                City cityOfInterest = FindCity(cityOfInterestName);

                for (int i = 1; i < parts.Length; i++) // Skip city of interest
                {
                    // Find adjacent city
                    City adjacentCity = FindCity(parts[i]);
                    if (adjacentCity != null)
                    {
                        cityOfInterest.AdjacentCities.Add(adjacentCity); //direct
                        adjacentCity.AdjacentCities.Add(cityOfInterest); //return
                    }
                }
            }
        }

        public City FindCity(string cityname)
        {
            foreach (var city in Cities)
            {
                if (city.CityName == cityname) return city;
            }
            return null;
        }

        // Hueristic Function
        public static double Distance(City city1, City city2)
        {
            double distance = Math.Sqrt(Math.Pow(city1.Longitude - city2.Longitude, 2) + Math.Pow(city1.Latitude - city2.Latitude, 2));
            return distance;
        }
    }
}
