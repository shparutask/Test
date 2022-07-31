using System;
using System.Collections.Generic;

namespace Test.Graph
{
    public class Node
    {
        public int BusNumber { get; set; }

        public int EdgeLength { get; set; }

        public List<TimeSpan> TimeTable { get; set; }
    }
}
