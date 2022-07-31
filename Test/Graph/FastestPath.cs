using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Graph
{
    public class FastestPath : SearchPathBase
    {
        TimeSpan[] times;

        public FastestPath(BusGraph routeFile, TimeSpan startTime) : base(routeFile)
        {
            times = new TimeSpan[_BusGraph.NodesCount];

            for (int i = 0; i < _BusGraph.NodesCount; i++)
            {
                times[i] = startTime;
            }
        }

        protected override void Search(ref int cur)
        {
            while (!_InTree[cur])
            {
                _InTree[cur] = true;
                for (int i = 0; i < _BusGraph.NodesCount; i++)
                {
                    if (_BusGraph.Graph[cur, i] != null)
                    {
                        int d = _Weight[cur] + _BusGraph.Graph[cur, i].EdgeLength + (_BusesRoute[cur] == _BusGraph.Graph[cur, i].BusNumber ? 0 : transferTime(_BusGraph.Graph[cur, i], times[cur]));
                        if (d < _Weight[i])
                        {
                            _Weight[i] = d;
                            _Parent[i] = cur;

                            times[i] = times[cur].Add(new TimeSpan(0, _BusGraph.Graph[cur, i].EdgeLength + (_BusesRoute[cur] == _BusGraph.Graph[cur, i].BusNumber ? 0 : transferTime(_BusGraph.Graph[cur, i], times[cur])), 0));
                            _BusesRoute[cur] = _BusGraph.Graph[cur, i].BusNumber;
                        }
                    }
                }
                int min_dist = int.MaxValue;
                for (int i = 0; i < _BusGraph.NodesCount; i++)
                {
                    if (!_InTree[i] && _Weight[i] < min_dist)
                    {
                        cur = i;
                        min_dist = _Weight[i];
                    }
                }
            }
        }

        protected override string GetResult(List<int> path)
        {
            return $"{string.Join(' ', path.ToArray())} { times[_Finish] }";
        }

        private int transferTime(Node curNode, TimeSpan curTime)
        {
            var departureTime = curNode.TimeTable.First(x => curTime <= x);

            return (int)departureTime.Add(-curTime).TotalMinutes;
        }
    }
}
