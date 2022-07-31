using System;
using System.Collections.Generic;
using System.Linq;
using Test.Helpers;
using Test.Model;

namespace Test.Graph
{
    public class BusGraph
    {
        Node[,] _Graph;
        int _NodesCount;
        List<Bus> _BusList;

        public int NodesCount { get { return _NodesCount; } }

        public Node[,] Graph { get { return _Graph; } }

        public List<Bus> BusList { get { return _BusList; } }

        public BusGraph(string routeFile)
        {
            var routeScheme = InputParcer.ParseInput(routeFile);
            _BusList = routeScheme.BusList;
            _Graph = new Node[routeScheme.StationCount, routeScheme.StationCount];
            _NodesCount = routeScheme.StationCount;

            for (int i = 0; i < _NodesCount; i++)
            {
                for (int j = 0; j < _NodesCount; j++)
                {
                    var bus = _BusList.FirstOrDefault(x => x.BusRoute.StationTimes.ContainsKey((i, j)));
                    if (bus == null)
                        continue;

                    var allPathLength = bus.BusRoute.StationTimes.Sum(x => x.Value);
                    var departureTime = bus.DepartureTime.Add(new TimeSpan(0, bus.BusRoute.StationTimes.TakeWhile(x => !(x.Key.Item1 == i && x.Key.Item2 == j)).Sum(x => x.Value), 0));
                    var timeTable = new List<TimeSpan>();

                    while (departureTime.Days < 1)
                    {
                        timeTable.Add(departureTime);
                        departureTime = departureTime.Add(new TimeSpan(0, allPathLength, 0));
                    }

                    _Graph[i, j] = new Node()
                    {
                        BusNumber = bus.BusNumber,
                        EdgeLength = bus.BusRoute.StationTimes[(i, j)],
                        TimeTable = timeTable
                    };
                }
            }
        }
    }
}
