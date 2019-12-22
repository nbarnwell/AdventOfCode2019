namespace FuelManagementSystem.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    public class OptimisationTests
    {
        [Test]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", ExpectedResult = 610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", ExpectedResult = 410)]
        public int Find_optimal_routes_to_intersection(string route1, string route2)
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            mapper.MapRoute(1, route1);
            mapper.MapRoute(2, route2);
            var optimiser = new Optimiser();
            var leastPointsToAnIntersection = optimiser.LeastDistanceToIntersection(mapper);
            return leastPointsToAnIntersection;
        }

        [Test]
        public void Puzzle3_part2()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);

            var routes = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle3\\input.txt");
            int routeId = 1;
            foreach (var route in routes)
            {
                mapper.MapRoute(routeId++, route);
            }

            var optimiser = new Optimiser();
            var leastPointsToAnIntersection = optimiser.LeastDistanceToIntersection(mapper);
            Console.WriteLine($"Least points to an intersection = {leastPointsToAnIntersection}");
            Assert.AreEqual(15612, leastPointsToAnIntersection);
        }
    }
}