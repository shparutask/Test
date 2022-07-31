using System;
using Test.Graph;

namespace Test
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the path of file with routes");
            string file = Console.ReadLine();

            Console.WriteLine("Enter the start station");
            var startStation = Convert.ToInt32(Console.ReadLine()) - 1;
            
            Console.WriteLine("Enter the finish station");
            var finishStation = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("Enter the start time");
            var startTime = TimeSpan.Parse(Console.ReadLine());

            var graph = new BusGraph(file);

            SearchPathBase pathSearcher = new FastestPath(graph, startTime);
            Console.WriteLine(pathSearcher.Dijkstra(startStation, finishStation));

            pathSearcher = new CheapestPath(graph);
            Console.WriteLine(pathSearcher.Dijkstra(startStation, finishStation));

            Console.ReadKey();
        }
    }
}
