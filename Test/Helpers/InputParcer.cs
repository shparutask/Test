using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test.Model;

namespace Test.Helpers
{
    public static class InputParcer
    {
        private static string _DepartureTimesKey = "DepartureTimes";
        private static string _PricesKey = "Prices";
        private static string _RouteKey = "Route";
        private static int _BusCount;
        private static int _StationCount;

        public static RouteScheme ParseInput(string inputFilePath)
        {
            var input = getInputStrings(inputFilePath);

            var departureTimes = input[_DepartureTimesKey].Split(' ').Select(x => TimeSpan.Parse(x)).ToArray();
            var prices = input[_PricesKey].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

            var busList = new List<Bus>();
            for (int i = 0; i < _BusCount; i++)
            {
                var routeStringSplitted = input[$"{_RouteKey}_{i}"].Split(' ');

                var stationCount = Convert.ToInt32(routeStringSplitted.First());
                var stations = routeStringSplitted.Skip(1).Take(stationCount).Select(x => Convert.ToInt32(x)).ToList();
                
                var stationsTimesString = routeStringSplitted.Skip(stationCount + 1).ToList();
                var stationTimes = new Dictionary<(int, int), int>();

                for (int j = 0; j < stationCount - 1; j++)
                {
                    stationTimes.Add((stations[j] - 1, stations[j + 1] - 1), Convert.ToInt32(stationsTimesString[j]));
                }

                stationTimes.Add((stations[stationCount - 1] - 1, stations[0] - 1), Convert.ToInt32(stationsTimesString[stationCount - 1]));

                var route = new Route()
                {
                    StationCount = stationCount,
                    Stations = stations,
                    StationTimes = stationTimes
                };

                busList.Add(new Bus()
                {
                    BusNumber = i + 1,
                    DepartureTime = departureTimes[i],
                    Price = prices[i],
                    BusRoute = route
                });
            }

            return new RouteScheme()
            {
                BusList = busList,
                StationCount = _StationCount
            };
        }

        static Dictionary<string, string> getInputStrings(string inputFilePath)
        {
            var result = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                _BusCount = Convert.ToInt32(reader.ReadLine());
                _StationCount = Convert.ToInt32(reader.ReadLine());

                result.Add(_DepartureTimesKey, reader.ReadLine());
                result.Add(_PricesKey, reader.ReadLine());

                int i = 0;
                while (!reader.EndOfStream)
                {
                    result.Add($"{_RouteKey}_{i}", reader.ReadLine());

                    i++;
                }

                return result;
            }
        }
    }
}
