using System.Collections.Generic;

namespace Test.Model
{
    public class Route
    {
        public int StationCount { get; set; }

        public List<int> Stations { get; set; } = new List<int>();

        public Dictionary<(int, int), int> StationTimes { get; set; } = new Dictionary<(int, int), int>();
    }
}
