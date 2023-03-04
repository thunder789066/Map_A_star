namespace Prg_1_map_A_star
{
    internal class Route
    {
        public Route()
        {
            Path = new List<City>();
        }
        public List<City> Path { get; set; }
        public double GetRouteDistance()
        {
            double distance = 0;
            for (int i = 1; i < Path.Count; i++) // Skip origin point
            {
                distance += Map.Distance(Path[i - 1], Path[i]);
            }
            return distance;
        }
    }
}
