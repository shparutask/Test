using System;
using System.Collections.Generic;
using System.Linq;
using Test.Model;

namespace Test.Graph
{
    public class CheapestPath : SearchPathBase
    {
        public CheapestPath(BusGraph busGraph) : base(busGraph)
        {

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
                        int c = cur;
                        int d = _Weight[cur] + (_BusesRoute[cur] == _BusGraph.Graph[cur, i].BusNumber ? 0 : _BusList.First(x => x.BusNumber == _BusGraph.Graph[c, i].BusNumber).Price);
                        if (d < _Weight[i])
                        {
                            _Weight[i] = d;
                            _Parent[i] = cur;

                            _BusesRoute[cur] = _BusGraph.Graph[cur, i].BusNumber;
                        }
                    }
                }
                int min_price = int.MaxValue;
                for (int i = 0; i < _BusGraph.NodesCount; i++)
                {
                    if (!_InTree[i] && _Weight[i] < min_price)
                    {
                        cur = i;
                        min_price = _Weight[i];
                    }
                }
            }
        }

        protected override string GetResult(List<int> path)
        {
            var travelCost = 0;
            var busNumber = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                var curBus = _BusList.First(x => x.BusNumber == _BusGraph.Graph[path[i] - 1, path[i + 1] - 1].BusNumber);

                if (busNumber != curBus.BusNumber)
                {
                    travelCost += curBus.Price;
                    busNumber = curBus.BusNumber;
                }
            }

            return $"{ string.Join(' ', path.ToArray()) } Price: {travelCost}";
        }
    }
}
