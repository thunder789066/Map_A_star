namespace Prg_1_map_A_star
{
    internal class City
    {
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<City> AdjacentCities { get; set; }
    }
}
