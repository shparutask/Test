using System;
using System.Collections.Generic;
using Test.Model;

namespace Test.Graph
{
    public class SearchPathBase
    {
        protected BusGraph _BusGraph;
        protected List<Bus> _BusList;

        protected int _Finish;

        protected int[] _Parent;
        protected bool[] _InTree;
        protected int[] _BusesRoute;
        protected int[] _Weight;

        public SearchPathBase(BusGraph busGraph)
        {
            _BusGraph = busGraph;
            _Parent = new int[_BusGraph.NodesCount];
            _InTree = new bool[_BusGraph.NodesCount];
            _BusesRoute = new int[_BusGraph.NodesCount];
            _Weight = new int[_BusGraph.NodesCount];
            _BusList = busGraph.BusList;

            for (int i = 0; i < _BusGraph.NodesCount; i++)
            {
                _Weight[i] = int.MaxValue;
            }
        }

        public string Dijkstra(int start, int finish)
        {
            int cur = start;
            _Weight[start] = 0;
            _Finish = finish;

            Search(ref cur);

            var path = getPath(start, finish);

            return GetResult(path);
        }

        protected virtual void Search(ref int cur) { }

        protected virtual string GetResult(List<int> path) { return string.Empty; }

        private List<int> getPath(int start, int finish)
        {
            List<int> path = new List<int>() { finish + 1 };
            int cur = finish;

            while (cur != start)
            {
                path.Insert(0, _Parent[cur] + 1);
                cur = _Parent[cur];
            }

            return path;
        }
    }
}
