using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FuelManagementSystem.Tests
{
    [TestFixture]
    public class MeasurementTests
    {
        [Test]
        public void Calculate_distance_between_coords()
        {
            IDistanceCalculator calculator = new ManhattanDistanceCalculator();
            var distance = calculator.GetDistance(Coords.From(0, 0), Coords.From(1, 1));
            Assert.AreEqual(2, distance);
        }

        [Test]
        public void Calculate_distance_between_different_coords()
        {
            IDistanceCalculator calculator = new ManhattanDistanceCalculator();
            var distance = calculator.GetDistance(Coords.From(0, 0), Coords.From(-21, 41));
            Assert.AreEqual(62, distance);
        }

        [Test]
        public void Puzzle3Example1()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            mapper.MapRoute(1, "R75,D30,R83,U83,L12,D49,R71,U7,L72");
            mapper.MapRoute(2, "U62,R66,U55,R34,D71,R55,D58,R83");
            var distanceCalcuator = new ManhattanDistanceCalculator();
            var result =
                mapper.GetCrossovers()
                    .Select(x => distanceCalcuator.GetDistance(Coords.From(0, 0), x))
                    .OrderBy(x => x)
                    .First();

            Assert.AreEqual(159, result);
        }

        [Test]
        public void Puzzle3Example2()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            mapper.MapRoute(1, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51");
            mapper.MapRoute(2, "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7");
            var distanceCalcuator = new ManhattanDistanceCalculator();
            var result =
                mapper.GetCrossovers()
                    .Select(x => distanceCalcuator.GetDistance(Coords.From(0, 0), x))
                    .OrderBy(x => x)
                    .First();

            Assert.AreEqual(135, result);
        }

        [Test]
        public void Puzzle3Result()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);

            var routes = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle3\\input.txt");
            int routeId = 1;
            foreach (var route in routes)
            {
                mapper.MapRoute(routeId++, route);
            }

            var distanceCalcuator = new ManhattanDistanceCalculator();
            var result =
                mapper.GetCrossovers()
                    .Select(x => distanceCalcuator.GetDistance(Coords.From(0, 0), x))
                    .OrderBy(x => x)
                    .First();

            Assert.AreEqual(260, result);
        }
    }
}