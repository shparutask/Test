using System;

namespace Test.Model
{
    public class Bus
    {
        public int BusNumber {get; set;}

        public TimeSpan DepartureTime { get; set; }

        public int Price { get; set; }

        public Route BusRoute { get; set; }
    }
}
