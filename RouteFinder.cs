namespace Prg_1_map_A_star
{
    internal class RouteFinder
    {
        private Map _map { get; set; }

        public RouteFinder(string coordinatesFile, string adjacenciesFile)
        {
            _map = new Map();
            _map.LoadFiles(coordinatesFile, adjacenciesFile);
        }

        public List<City> getCityList()
        {
            return _map.Cities;
        }

        public Route FindRoute(string startingCityName, string endingCityName)
        {
            City startingCity = _map.FindCity(startingCityName);
            Route route = new Route();
            route.Path.Add(startingCity);
            if (startingCity.CityName == endingCityName) return route;

            //Check for bad input, also check if startingCityName==endingCityName, also check if these are in the list
            if (startingCity == null)
            {
                // Complain that city wasn't valid
                throw new ArgumentException("Starting City is invalid");
            }
            if (endingCityName == null)
            {
                // Complain that city wasn't valid
                throw new ArgumentException("Ending City is invalid");
            }

            City currentCity = startingCity;
            List<City> openCityList = new List<City>();
            List<City> closedCityList = new List<City>();

            openCityList.AddRange(startingCity.AdjacentCities);
            closedCityList.Add(startingCity);

            // A* Algorithm
            while (openCityList.Count > 0)
            {
                City nextCity = FindClosestToDestination(openCityList, currentCity);
                route.Path.Add(nextCity);
                if (nextCity != null)
                {
                    // Move to next city
                    currentCity = nextCity;
                    if (currentCity.CityName == endingCityName) return route; // Are we there yet? if so finish here

                    // Not there yet. Work on finding the next city.
                    MoveToClosedList(currentCity, openCityList, closedCityList);
                    foreach (City city in currentCity.AdjacentCities)
                    {
                        if (city.CityName == currentCity.CityName) continue;
                        if (!City.Contains(city, openCityList) && !City.Contains(city, closedCityList))
                        {
                            openCityList.Add(city);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            // No valid route found
            return null;
        }

        private void MoveToClosedList(City city, List<City> list1, List<City> list2)
        {
            list1.Remove(city);
            list2.Add(city);
        }

        private City FindClosestToDestination(List<City> cities, City destination)
        {
            if (cities.Count == 0)
            {
                return null; //No cities left to choose from
            }

            City best = cities[0];
            double bestDistance = Map.Distance(best, destination);
            foreach (City city in cities)
            {
                if (best.CityName == city.CityName) continue;
                if (bestDistance < Map.Distance(city, destination))
                {
                    best = city;
                    bestDistance = Map.Distance(best, destination);
                }
            }
            return best;
        }
    }
}
